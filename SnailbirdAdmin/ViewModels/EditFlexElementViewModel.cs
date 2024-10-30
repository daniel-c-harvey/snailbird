using Amazon.Runtime.Internal.Transform;
using Newtonsoft.Json.Serialization;
using SnailbirdData.Models.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnailbirdAdmin.ViewModels
{
    public class FlexElementChoice
    {
        public FlexElement Element { get; }
        public Func<FlexElement> Build { get; }

        public FlexElementChoice(FlexElement element)
        {
            Element = element;
            Build = () => (FlexElement?)Activator.CreateInstance(type) ?? throw new Exception();
        }
    }

    public class EditFlexElementViewModel
    {
        public FlexElementChoice Choice { get; }

        public EditFlexElementViewModel(FlexElement element)
        {
            Choice = new FlexElementChoice(element);
        }

        public void ReplaceElement()
        {
            
        }
    }
}
