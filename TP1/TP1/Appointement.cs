namespace TP1
{
   /// <summary>
   /// Représente un rendez-vous entre un patient et un médecin
   /// </summary>
   class Appointment
   {
      /// <summary>Durée en minutes de la catégorie A</summary>
      public const int DurationA = 10;

      /// <summary>Durée en minutes de la catégorie B</summary>
      public const int DurationB = 20;

      /// <summary>Durée en minutes de la catégorie C</summary>
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

      /// <summary>Patient du rendez-vous</summary>
      public Patient Patient { get { return _patient; } }

      /// <summary>Médecin du rendez-vous</summary>
      public Doctor Doctor { get { return _doctor; } }

      /// <summary>Date et heure du rendez-vous</summary>
      public DateTime DateTime { get { return _dateTime; } }

      /// <summary>Catégorie du rendez-vous (A, B ou C)</summary>
      public char Category { get { return _category; } }

      /// <summary>Durée en minutes du rendez-vous</summary>
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
