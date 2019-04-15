namespace Evoq.Surfdude
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public sealed class SendDictionary : Dictionary<string, string>
    {
        public SendDictionary()
        {
        }

        public SendDictionary(IDictionary<string, string> pairs)
            : base(pairs)
        {
            if (pairs == null)
            {
                throw new ArgumentNullException(nameof(pairs));
            }
        }

        public SendDictionary(object sendModel) 
            : base(CreateDictionaryFrom(sendModel))
        {
            if (sendModel == null)
            {
                throw new ArgumentNullException(nameof(sendModel));
            }
        }

        private static IDictionary<string, string> CreateDictionaryFrom(object sendModel)
        {
            var sendProperties = sendModel.GetType().GetProperties().ToArray();
            var sendBag = new Dictionary<string, string>(sendProperties.Length);

            foreach (var property in sendProperties)
            {
                sendBag.Add(property.Name, property.GetValue(sendModel).ToString());
            }

            return sendBag;
        }
    }
}
