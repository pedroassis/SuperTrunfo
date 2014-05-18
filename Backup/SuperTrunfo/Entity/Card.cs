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
        public String hability;
        public String force;
        public String velocity;
        public String equipment;
        public String inteligence;
		
		public Card ()
		{
			
		}
		
		public Card (String id, String name, String hability, String force, String velocity, String equipment, String inteligence){
			this.id 		= id;
			this.name 		= name;
			this.hability 	= hability;
			this.force 		= force;
			this.velocity 	= velocity;
			this.equipment 	= equipment;
			this.inteligence= inteligence;
		}
    }
}
