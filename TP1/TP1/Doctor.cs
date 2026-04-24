namespace TP1
{
   /// <summary>
   /// Représente un médecin du centre médical
   /// </summary>
   class Doctor : Person
   {
      // Nombre maximum de patients qu'un médecin peut prendre en charge
      public const int MaxPatients = 3;

      // Ligne de séparation pour l'affichage
      private const string DashLine = "-----------------------------";

      /// <summary>
      /// Constructeur à partir de la saisie de l'utilisateur
      /// </summary>
      public Doctor() : base()
      {
      }

      /// <summary>
      /// Constructeur à partir du fichier de sauvegarde
      /// </summary>
      /// <param name="id">Identifiant du médecin</param>
      /// <param name="firstName">Prénom</param>
      /// <param name="lastName">Nom</param>
      public Doctor(int id, string firstName, string lastName) : base(id, firstName, lastName)
      {
      }

      // Indique si le médecin peut encore prendre en charge de nouveaux patients
      public bool IsAvailable { get { return _patients.Count < MaxPatients; } }

      // Nombre de patients actuellement pris en charge
      public int PatientCount { get { return _patients.Count; } }

      /// <summary>
      /// Ajoute un patient à la liste des patients du médecin
      /// </summary>
      /// <param name="patient">Patient à ajouter</param>
      public void AddPatient(Patient patient)
      {
         _patients.Add(patient);
      }

      /// <summary>
      /// Affiche les informations du médecin avec son numéro dans la liste
      /// </summary>
      /// <param name="index">Numéro d'affichage (position dans la liste)</param>
      public void Print(int index)
      {
         Console.WriteLine($"  {index}) {FirstName} {LastName} ({_patients.Count}/{MaxPatients} patients)");

         // Affiche les rendez-vous s'il y en a
         if (Schedule.AppointmentCount > 0)
            Schedule.Print();
      }

      private readonly List<Patient> _patients = new();
   }
}
