using System;
using System.Collections.Generic;
using System.Text;

namespace Intranet2.Helpers
{
    public static class USStates
    {
        static List<USState> states;

        static USStates()
        {
            states = new List<USState>(50);
            states.Add(new USState("AL", "Alabama"));
            states.Add(new USState("AK", "Alaska"));
            states.Add(new USState("AZ", "Arizona"));
            states.Add(new USState("AR", "Arkansas"));
            states.Add(new USState("CA", "California"));
            states.Add(new USState("CO", "Colorado"));
            states.Add(new USState("CT", "Connecticut"));
            states.Add(new USState("DE", "Delaware"));
            states.Add(new USState("DC", "District Of Columbia"));
            states.Add(new USState("FL", "Florida"));
            states.Add(new USState("GA", "Georgia"));
            states.Add(new USState("HI", "Hawaii"));
            states.Add(new USState("ID", "Idaho"));
            states.Add(new USState("IL", "Illinois"));
            states.Add(new USState("IN", "Indiana"));
            states.Add(new USState("IA", "Iowa"));
            states.Add(new USState("KS", "Kansas"));
            states.Add(new USState("KY", "Kentucky"));
            states.Add(new USState("LA", "Louisiana"));
            states.Add(new USState("ME", "Maine"));
            states.Add(new USState("MD", "Maryland"));
            states.Add(new USState("MA", "Massachusetts"));
            states.Add(new USState("MI", "Michigan"));
            states.Add(new USState("MN", "Minnesota"));
            states.Add(new USState("MS", "Mississippi"));
            states.Add(new USState("MO", "Missouri"));
            states.Add(new USState("MT", "Montana"));
            states.Add(new USState("NE", "Nebraska"));
            states.Add(new USState("NV", "Nevada"));
            states.Add(new USState("NH", "New Hampshire"));
            states.Add(new USState("NJ", "New Jersey"));
            states.Add(new USState("NM", "New Mexico"));
            states.Add(new USState("NY", "New York"));
            states.Add(new USState("NC", "North Carolina"));
            states.Add(new USState("ND", "North Dakota"));
            states.Add(new USState("OH", "Ohio"));
            states.Add(new USState("OK", "Oklahoma"));
            states.Add(new USState("OR", "Oregon"));
            states.Add(new USState("PA", "Pennsylvania"));
            states.Add(new USState("RI", "Rhode Island"));
            states.Add(new USState("SC", "South Carolina"));
            states.Add(new USState("SD", "South Dakota"));
            states.Add(new USState("TN", "Tennessee"));
            states.Add(new USState("TX", "Texas"));
            states.Add(new USState("UT", "Utah"));
            states.Add(new USState("VT", "Vermont"));
            states.Add(new USState("VA", "Virginia"));
            states.Add(new USState("WA", "Washington"));
            states.Add(new USState("WV", "West Virginia"));
            states.Add(new USState("WI", "Wisconsin"));
            states.Add(new USState("WY", "Wyoming"));
        }

        public static string[] Abbreviations()
        {
            List<string> abbrevList = new List<string>(states.Count);
            foreach (var state in states)
            {
                abbrevList.Add(state.Abbreviation);
            }
            return abbrevList.ToArray();
        }

        public static string[] Names()
        {
            List<string> nameList = new List<string>(states.Count);
            foreach (var state in states)
            {
                nameList.Add(state.Name);
            }
            return nameList.ToArray();
        }

        public static Dictionary<string, string> AsDictionary()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach(var state in states)
            {
                result.Add(state.Abbreviation, state.Name);
            }
            return result;
        }

        public static USState[] States()
        {
            return states.ToArray();
        }

    }

    public class USState
    {

        public USState()
        {
            Name = null;
            Abbreviation = null;
        }

        public USState(string ab, string name)
        {
            Name = name;
            Abbreviation = ab;
        }

        public string Name { get; set; }

        public string Abbreviation { get; set; }

        public override string ToString()
        {
            return string.Format("{0} - {1}", Abbreviation, Name);
        }

    }
}
