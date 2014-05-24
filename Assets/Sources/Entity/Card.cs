using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperTrunfo
{
    class Card
    {
        public String id;

        public String name;
        public int hability;
        public int power;
        public int velocity;
        public int equipment;
        public int inteligence;
		
		public Card ()
		{
			
		}

        public Card(String id, String name, int hability, int power, int velocity, int equipment, int inteligence)
        {
			this.id 		= id;
			this.name 		= name;
			this.hability 	= hability;
			this.power 		= power;
			this.velocity 	= velocity;
			this.equipment 	= equipment;
			this.inteligence= inteligence;
		}
    }
}
