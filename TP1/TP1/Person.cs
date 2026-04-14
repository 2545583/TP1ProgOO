namespace TP1
{
     class Person
    {
        public Person()
        {
            
            
        }


        public void AskInformations(string firstName, string lastName)
        {
            Console.Write("Indiquer votre nom: ");
             firstName = Console.ReadLine();
            Console.Write("Indiquer votre prénom: ");
             lastName= Console.ReadLine();
        }



        public string Name { get { return _firstName; } }   // accessible de l'extérieur
        public string Prenom { get { return _lastName; } }

        private string _firstName;
        private string _lastName;
        private static int _id;

    }
}
