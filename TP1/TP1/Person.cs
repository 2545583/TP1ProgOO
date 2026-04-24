namespace TP1
{
   /// <summary>
   /// Représente une personne du système (médecin ou patient)
   /// Responsable d'attribuer un identifiant unique à toutes les personnes
   /// </summary>
   class Person
   {
      private static int _nextId = 1;

      /// <summary>
      /// Constructeur à partir de la saisie de l'utilisateur
      /// Demande le prénom et le nom, et attribue un identifiant unique
      /// </summary>
      protected Person()
      {
         _id = _nextId++;

         Console.Write("Indiquez le prénom: ");
         _firstName = ReadName();

         Console.Write("Indiquez le nom: ");
         _lastName = ReadName();
      }

      /// <summary>
      /// Constructeur à partir du fichier de sauvegarde
      /// </summary>
      /// <param name="id">Identifiant de la personne</param>
      /// <param name="firstName">Prénom</param>
      /// <param name="lastName">Nom</param>
      protected Person(int id, string firstName, string lastName)
      {
         if (id >= _nextId)
         {
                _nextId = id + 1;
         }

            _id = id;
         _firstName = firstName;
         _lastName = lastName;
      }

      //Identifiant unique de la personne
      public int Id { get { return _id; } }

      // Prénom de la personne
      public string FirstName { get { return _firstName; } }

      // Nom de la personne
      public string LastName { get { return _lastName; } }

      // Horaire des rendez-vous de la personne
      public Schedule Schedule { get { return _schedule; } }

      // Lit et valide un nom depuis la console
      private static string ReadName()
      {
         string value = Console.ReadLine()?.Trim() ?? "";
         if (value.Length == 0)
         {
             throw new FormatException("Le nom ne peut pas être vide");
         }
        if (value.Contains(';'))
        {
            throw new FormatException("Le nom ne peut pas contenir le caractère ';'");
        }
            return value;
      }

      private readonly int _id;
      private readonly string _firstName;
      private readonly string _lastName;
      private readonly Schedule _schedule = new();
   }
}
