namespace TP1
{
   /// <summary>
   /// Représente un rendez-vous entre un patient et un médecin
   /// </summary>
   class Appointment
   {
      // Durée en minutes de la catégorie A
      public const int DurationA = 10;

      // Durée en minutes de la catégorie B
      public const int DurationB = 20;

      // Durée en minutes de la catégorie C
      public const int DurationC = 30;

      /// <summary>
      /// Constructeur
      /// </summary>
      /// <param name="patient">Patient du rendez-vous</param>
      /// <param name="doctor">Médecin du rendez-vous</param>
      /// <param name="dateTime">Date et heure du rendez-vous</param>
      /// <param name="category">Catégorie du rendez-vous (A, B ou C)</param>
      public Appointment(Patient patient, Doctor doctor, DateTime dateTime, char category)
      {
         _patient = patient;
         _doctor = doctor;
         _dateTime = dateTime;
         _category = category;
         _duration = GetDuration(category);
      }

      // Patient du rendez-vous
      public Patient Patient { get { return _patient; } }

      // Médecin du rendez-vous
      public Doctor Doctor { get { return _doctor; } }

      // Date et heure du rendez-vous
      public DateTime DateTime { get { return _dateTime; } }

      // Catégorie du rendez-vous (A, B ou C)
      public char Category { get { return _category; } }

      // Durée en minutes du rendez-vous
      public int Duration { get { return _duration; } }

      /// <summary>
      /// Retourne la durée en minutes correspondant à la catégorie donnée
      /// </summary>
      /// <param name="category">Catégorie (A, B ou C)</param>
      /// <returns>Durée en minutes</returns>
      /// <exception cref="FormatException">Si la catégorie est invalide</exception>
      public static int GetDuration(char category)
      {
         switch (category)
         {
            case 'A': return DurationA;
            case 'B': return DurationB;
            case 'C': return DurationC;
            default: throw new FormatException("Catégorie invalide");
         }
      }

      /// <summary>
      /// Affiche les informations du rendez-vous
      /// </summary>
      public void Print()
      {
         Console.WriteLine($"     {_dateTime:yyyy-MM-dd HH:mm:ss} ({_duration} minutes) pour patient {_patient.Id} et médecin {_doctor.Id}");
      }

      private readonly Patient _patient;
      private readonly Doctor _doctor;
      private readonly DateTime _dateTime;
      private readonly char _category;
      private readonly int _duration;
   }
}
