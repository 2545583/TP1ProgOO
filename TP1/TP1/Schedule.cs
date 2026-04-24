namespace TP1
{
   /// <summary>
   /// Contient tous les rendez-vous d'une personne, en ordre chronologique croissant
   /// </summary>
   class Schedule
   {
      // Nombre de rendez-vous dans l'horaire
      public int AppointmentCount { get { return _appointments.Count; } }

        /// <summary>
        /// Vérifie si un rendez-vous existe déjà à la date et heure données
        /// </summary>
        /// <param name="dateTime">Date et heure à vérifier</param>
        /// <returns>Vrai s'il y a un conflit, faux sinon</returns>
        public bool VerifyTime(DateTime dateTime)
        {
            foreach (var appt in _appointments)
            {
                if (appt.DateTime == dateTime)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Ajoute un rendez-vous en maintenant l'ordre chronologique croissant
        /// </summary>
        /// <param name="appointment">Rendez-vous à ajouter</param>
        public void AddAppointment(Appointment appointment)
        {
         // On cherche la position d'insertion pour garder l'ordre croissant
         int i = 0;
            while (i < _appointments.Count && _appointments[i].DateTime < appointment.DateTime)
            {
                i++;
                _appointments.Insert(i, appointment);
            }
            
        }

      /// <summary>
      /// Affiche tous les rendez-vous de l'horaire
      /// </summary>
      public void Print()
      {
         foreach (var appt in _appointments)
         {
                appt.Print();
         }
        }

      private readonly List<Appointment> _appointments = new();
   }
}
