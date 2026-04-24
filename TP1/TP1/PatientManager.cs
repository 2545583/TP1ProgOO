namespace TP1
{
   /// <summary>
   /// Gère la liste de tous les patients du système
   /// Responsable de l'ajout, l'affichage, la sauvegarde et la prise de rendez-vous
   /// </summary>
   class PatientManager
   {
      private const string FileName = "../../../patients.txt";
      private const char Separator = ';';
      private const string DashLine = "-----------------------------";

      /// <summary>
      /// Constructeur — lit le fichier de sauvegarde des patients s'il existe
      /// </summary>
      /// <param name="doctorMgr">Gestionnaire de médecins (pour lier les patients à leur médecin)</param>
      public PatientManager(DoctorManager doctorMgr)
      {
         _doctorMgr = doctorMgr;

         if (!File.Exists(FileName))
            return;

         using (StreamReader input = new(FileName))
         {
            string? line = input.ReadLine();
            while (line != null)
            {
               line = line.Trim();
               if (line.Length > 0)
               {
                  try
                  {
                     string[] values = line.Split(Separator);
                     if (values.Length != 4)
                        throw new InvalidDataException("Format invalide");

                     int id = int.Parse(values[0].Trim());
                     string firstName = values[1].Trim();
                     string lastName = values[2].Trim();
                     int doctorId = int.Parse(values[3].Trim());

                     Patient patient = new(id, firstName, lastName);

                     // Relie le patient à son médecin si applicable
                     if (doctorId != -1)
                     {
                        Doctor? doctor = _doctorMgr.FindById(doctorId);
                        if (doctor != null)
                        {
                           patient.AssignDoctor(doctor);
                           doctor.AddPatient(patient);
                        }
                     }

                     _patients.Add(patient);
                  }
                  catch (Exception e)
                  {
                     Console.WriteLine($"Erreur de chargement d'un patient: {e.Message}");
                  }
               }
               line = input.ReadLine();
            }
         }
      }

      /// <summary>
      /// Demande les informations à l'utilisateur et ajoute un nouveau patient
      /// Assigne automatiquement un médecin disponible, ou place en attente
      /// </summary>
      public void Add()
      {
         try
         {
            Patient patient = new();

            // Assigne un médecin disponible si possible
            Doctor? doctor = _doctorMgr.FindAvailableDoctor();
            if (doctor != null)
            {
               patient.AssignDoctor(doctor);
               doctor.AddPatient(patient);
            }

            _patients.Add(patient);
            Console.WriteLine($"Patient {patient.Id} ajouté.");
         }
         catch (FormatException e)
         {
            Console.WriteLine(e.Message);
         }
      }

      /// <summary>
      /// Affiche tous les patients en deux catégories : avec médecin et sans médecin
      /// </summary>
      public void Print()
      {
         // Sépare les patients en deux listes
         List<Patient> withDoctor = new();
         List<Patient> waiting = new();
         foreach (var patient in _patients)
         {
            if (patient.HasDoctor)
               withDoctor.Add(patient);
            else
               waiting.Add(patient);
         }

         Console.WriteLine($"Patients avec médecin ({withDoctor.Count})");
         Console.WriteLine(DashLine);
         int index = 1;
         foreach (var patient in withDoctor)
            patient.Print(index++);

         Console.WriteLine();

         Console.WriteLine($"Patients sans médecin ({waiting.Count})");
         Console.WriteLine(DashLine);
         index = 1;
         foreach (var patient in waiting)
            patient.Print(index++);
      }

      /// <summary>
      /// Sauvegarde tous les patients dans le fichier de données
      /// Les rendez-vous ne sont pas sauvegardés
      /// </summary>
      public void Save()
      {
         using (StreamWriter output = new(FileName))
         {
            foreach (var patient in _patients)
            {
               // -1 indique qu'aucun médecin n'est assigné
               int doctorId = patient.HasDoctor ? patient.Doctor!.Id : -1;
               output.WriteLine($"{patient.Id}{Separator}{patient.FirstName}{Separator}{patient.LastName}{Separator}{doctorId}");
            }
         }
      }

      /// <summary>
      /// Demande les informations nécessaires et crée un rendez-vous pour un patient
      /// </summary>
      public void MakeAppointment()
      {
         Console.Write("Indiquez l'identifiant du patient: ");
         if (!int.TryParse(Console.ReadLine(), out int patientId))
         {
            Console.WriteLine("Identifiant invalide");
            return;
         }

         Patient? patient = FindPatientById(patientId);
         if (patient == null)
         {
            Console.WriteLine("Patient introuvable");
            return;
         }

         Console.Write("Indiquez la date et l'heure voulue (A/M/J H:M): ");
         string? dateTimeStr = Console.ReadLine()?.Trim();
         if (!DateTime.TryParse(dateTimeStr, out DateTime dateTime))
         {
            Console.WriteLine("Date ou heure invalide");
            return;
         }

         Console.Write("Indiquez la catégorie de rendez-vous: A (10 minutes), B (20 minutes) ou C (30 minutes): ");
         string? catInput = Console.ReadLine()?.Trim().ToUpper();
         if (catInput == null || catInput.Length == 0 ||
             (catInput[0] != 'A' && catInput[0] != 'B' && catInput[0] != 'C'))
         {
            Console.WriteLine("Catégorie invalide");
            return;
         }
         char category = catInput[0];

         // Détermine le médecin pour le rendez-vous
         Doctor? doctor;
         if (patient.HasDoctor)
         {
            doctor = patient.Doctor;
         }
         else
         {
            doctor = _doctorMgr.FindBestDoctorForAppointment();
            if (doctor == null)
            {
               Console.WriteLine("Aucun médecin disponible pour ce rendez-vous");
               return;
            }
         }

         // Vérifie les conflits d'horaire
         if (doctor!.Schedule.HasConflict(dateTime))
         {
            Console.WriteLine("Conflit d'horaire avec le médecin");
            return;
         }
         if (patient.Schedule.HasConflict(dateTime))
         {
            Console.WriteLine("Conflit d'horaire avec le patient");
            return;
         }

         // Crée le rendez-vous et l'ajoute aux deux horaires
         Appointment appointment = new(patient, doctor, dateTime, category);
         doctor.Schedule.AddAppointment(appointment);
         patient.Schedule.AddAppointment(appointment);

         Console.WriteLine("Rendez-vous ajouté.");
         appointment.Print();
      }

      /// <summary>
      /// Cherche un patient par son identifiant
      /// </summary>
      /// <param name="id">Identifiant recherché</param>
      /// <returns>Le patient trouvé, ou null s'il n'existe pas</returns>
      private Patient? FindPatientById(int id)
      {
         foreach (var patient in _patients)
            if (patient.Id == id)
               return patient;
         return null;
      }

      private readonly DoctorManager _doctorMgr;
      private readonly List<Patient> _patients = new();
   }
}
