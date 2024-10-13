using Core;
using DataAccess;
using MongoDB.Driver;
using Newtonsoft.Json;
using SnailbirdUtility.Converters;
using SnailbirdData.Adapters;
using SnailbirdData.DataAdapters;
using SnailbirdData.Models.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScratchConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //LiveJamPost x = new LiveJamPost
            //    (1,
            //    "Live Session March 27",
            //    new DateTime(2024, 3, 27),
            //    @"Making heavy industrial drums and bass sounds is something I've struggled with for a long time despite loving the style.  
            //     This was a test of some ideas for a heavier rhythm section.  The synth and drum parts are inspired by stamping plants, 
            //     emergency alarms, and a sense of urgency and panic.  I ended up loving the simple acid ostinato in 5/16 and the 
            //     polymetric modulation that occurs as a result.  I hope you enjoy too.",
            //    @"https://www.youtube.com/embed/_Vm64ueALkE?si=V8LU9I1cPMeClJcU", [
            //        new LiveJamPostInstrument("Title", "Flint Metal Center"),
            //        new LiveJamPostInstrument("Drums", "TR-8S, kit is custom samples synthesized in Vital/Serum/Phase Plant, and the 909 ACB Ride, smashed by the mixer preamp on high gain."),
            //        new LiveJamPostInstrument("Rumble Sub Bass", "Hydrasynth -> TR-8S ext side chain for pumping compression"),
            //        new LiveJamPostInstrument("Acid", "TD-3 -> GE-7 -> Badwater -> MS-60B -> QuadraVerb"),
            //        new LiveJamPostInstrument("Procedural Arp Lead", "Neutron -> MS70-CDR -> QuadraVerb"),
            //        new LiveJamPostInstrument("Swarm Cluster Lead", "Minilogue XD (internal FX)")
            //    ]);

            //JsonSerializer serializer = new JsonSerializer() { Formatting = Formatting.Indented };
            //serializer.Serialize(x);
            //Console.WriteLine(JsonConvert.SerializeObject(x));

            //var x = new MongoObject<LiveJamPost>() { Document = new LiveJamPost() { ID = 12345678} };

            LiveJamPostToFlexPostConverter.ConvertLiveJamPostToFlexPosts("snailbird-dev");

            //Connections connections = 
            //    new Connections 
            //    { 
            //        ConnectionStrings =
            //        [
            //            new Connection { ID = 1, Name = "snailbird-dev", ConnectionString = "test" },
            //            new Connection { ID = 2, Name = "snailbird", ConnectionString = "test" }
            //        ] 
            //    };
            //ConnectionStringTools.SaveToFile("../../../.secrets/connections.json", connections);
        }
    }
}