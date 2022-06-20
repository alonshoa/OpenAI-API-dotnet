using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;

namespace OpenAI_API.Edit
{

    [DataContract]
    public class Choice
    {
        [DataMember]
        public string text { get; set; }
        [DataMember]
        public int index { get; set; }
    }

    [DataContract]
    public class EditResponse
    {
        [DataMember]
        public string object1 { get; set; }
        [DataMember]
        public int created { get; set; }
        [DataMember]
        public List<Choice> choices { get; set; }
    }

    [Serializable]
    public class EditRequest
    {
        public string model { get; set; }
        public string instruction { get; set; }
        public string input { get; set; }
        public int n { get; set; }
        public float temperature { get; set; }
        public float top_p { get; set; }

        public EditRequest()
        {
            this.model = "code-davinci-edit-001";
            this.instruction = "create a function that prints all names in a list";
            this.input = "";
            this.n = 1;
        }

        public EditRequest(string model, string instruction)
        {
            this.model = model;
            this.instruction = instruction;
        }

        public EditRequest(string model, string instruction, string input)
        {
            this.model = model;
            this.instruction = instruction;
            this.input = input;
        }

        public EditRequest(string model, string instruction, string input, int n)
        {
            this.model = model;
            this.instruction = instruction;
            this.input = input;
            this.n = n;
        }

        public EditRequest(string model, string instruction, string input, int n, float temperature)
        {
            this.model = model;
            this.instruction = instruction;
            this.input = input;
            this.n = n;
            this.temperature = temperature;
        }

        public EditRequest(string model, string instruction, string input, int n, float temperature, float top_p)
        {
            this.model = model;
            this.instruction = instruction;
            this.input = input;
            this.n = n;
            this.temperature = temperature;
            this.top_p = top_p;
        }
    }
}