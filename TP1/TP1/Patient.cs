namespace TP1
{
   /// <summary>
   /// Représente un patient du centre médical
   /// </summary>
   class Patient : Person
   {
      /// <summary>
      /// Constructeur à partir de la saisie de l'utilisateur
      /// </summary>
      public Patient() : base()
      {
      }

      /// <summary>
      /// Constructeur à partir du fichier de sauvegarde
      /// </summary>
      /// <param name="id">Identifiant du patient</param>
      /// <param name="firstName">Prénom</param>
      /// <param name="lastName">Nom</param>
      public Patient(int id, string firstName, string lastName) : base(id, firstName, lastName)
      {
      }

      // Médecin assigné au patient, ou null si en attente
      public Doctor? Doctor { get { return _doctor; } }

      // Indique si le patient a un médecin assigné
      public bool HasDoctor { get { return _doctor != null; } }

      /// <summary>
      /// Assigne un médecin au patient
      /// </summary>
      /// <param name="doctor">Médecin à assigner</param>
      public void AssignDoctor(Doctor doctor)
      {
         _doctor = doctor;
      }

      /// <summary>
      /// Affiche les informations du patient avec son numéro dans la liste
      /// </summary>
      /// <param name="index">Numéro d'affichage (position dans la liste)</param>
      public void Print(int index)
      {
         if (_doctor != null)
         {
            Console.WriteLine($"  {index}) {FirstName} {LastName}, médecin: Dr. {_doctor.FirstName} {_doctor.LastName}");
         }
         else
         {
            Console.WriteLine($"  {index}) {FirstName} {LastName}, en attente d'un médecin");

         }

        // Affiche les rendez-vous s'il y en a
        if (Schedule.AppointmentCount > 0)
        {
            Schedule.Print();
        }
        }

      private Doctor? _doctor = null;
   }
}
