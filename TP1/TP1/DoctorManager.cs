namespace TP1
{
   /// <summary>
   /// Gère la liste de tous les médecins du système
   /// Responsable de l'ajout, l'affichage et la sauvegarde des médecins
   /// </summary>
   class DoctorManager
   {
 

      /// <summary>
      /// Constructeur  lit le fichier de sauvegarde des médecins s'il existe
      /// </summary>
      public DoctorManager()
      {
            if (!File.Exists(FileName))
            {
                return;
            }

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
                     if (values.Length != 3)
                     {
                         throw new InvalidDataException("Format invalide");
                     }

                     int id = int.Parse(values[0].Trim());
                     string firstName = values[1].Trim();
                     string lastName = values[2].Trim();

                     _doctors.Add(new Doctor(id, firstName, lastName));
                  }
                  catch (Exception e)
                  {
                     Console.WriteLine($"Erreur de chargement d'un médecin: {e.Message}");
                  }
               }
               line = input.ReadLine();
            }
         }
      }

      /// <summary>
      /// Demande les informations à l'utilisateur et ajoute un nouveau médecin
      /// </summary>
      public void Add()
      {
         try
         {
            Doctor doctor = new();
            _doctors.Add(doctor);
            Console.WriteLine($"Médecin {doctor.Id} ajouté.");
         }
         catch (FormatException e)
         {
            Console.WriteLine(e.Message);
         }
      }

      /// <summary>
      /// Affiche tous les médecins en deux catégories : disponibles et non disponibles
      /// </summary>
      public void Print()
      {
         // Affichage des médecins disponibles
         List<Doctor> available = new();
         List<Doctor> unavailable = new();
         foreach (var doctor in _doctors)
         {
            if (doctor.IsAvailable)
               available.Add(doctor);
            else
               unavailable.Add(doctor);
         }

         Console.WriteLine($"Médecins disponibles ({available.Count})");
         Console.WriteLine(DashLine);
         int index = 1;
         foreach (var doctor in available)
         {
             doctor.Print(index++);
         }

            Console.WriteLine();

         // Affichage des médecins non disponibles
         Console.WriteLine($"Médecins non disponibles ({unavailable.Count})");
         Console.WriteLine(DashLine);
         index = 1;
         foreach (var doctor in unavailable)
         {
            doctor.Print(index++);
         }
        }

      /// <summary>
      /// Sauvegarde tous les médecins dans le fichier de données
      /// </summary>
      public void Save()
      {
         using (StreamWriter output = new(FileName))
         {
            foreach (var doctor in _doctors)
                {
                    output.WriteLine($"{doctor.Id}{Separator}{doctor.FirstName}{Separator}{doctor.LastName}");
                }
            }
      }

      /// <summary>
      /// Retourne le médecin correspondant à l'identifiant donné
      /// </summary>
      /// <param name="id">Identifiant recherché</param>
      /// <returns>Le médecin trouvé, ou null s'il n'existe pas</returns>
      public Doctor? FindById(int id)
      {
         foreach (var doctor in _doctors)
            if (doctor.Id == id)
            {
                    return doctor;
            }
            return null;
      }

      /// <summary>
      /// Retourne le premier médecin disponible pour prendre en charge un nouveau patient
      /// </summary>
      /// <returns>Un médecin disponible, ou null si aucun</returns>
      public Doctor? FindAvailableDoctor()
      {
         foreach (var doctor in _doctors)
            if (doctor.IsAvailable)
            {
                return doctor;
            }
            return null;
      }

      /// <summary>
      /// Retourne le médecin disponible avec le moins de rendez-vous
      /// En cas d'égalité, celui avec le moins de patients
      /// </summary>
      /// <returns>Le meilleur médecin disponible, ou null si aucun</returns>
      public Doctor? FindBestDoctorForAppointment()
      {
         Doctor? best = null;
         foreach (var doctor in _doctors)
         {
            if (!doctor.IsAvailable)
            {
                continue;
            }
                if (best == null ||
                doctor.Schedule.AppointmentCount < best.Schedule.AppointmentCount ||
                (doctor.Schedule.AppointmentCount == best.Schedule.AppointmentCount &&
                 doctor.PatientCount < best.PatientCount))
                {
               best = doctor;
                }
         }
         return best;
      }

      private readonly List<Doctor> _doctors = new();
      private const string FileName = "../../../doctors.txt";
      private const char Separator = ';';
      private const string DashLine = "-----------------------------";
    }
}
