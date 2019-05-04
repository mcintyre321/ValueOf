using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValueOf.Tests
{
    [TypeConverter(typeof(ValueOfTypeConverter<string, ClientId>))]
    public class ClientId : ValueOf<string, ClientId> { }

    public class ClientIdWithoutTypeConverter : ValueOf<string, ClientIdWithoutTypeConverter> { }


    public class ClientIdModel
    {
        public ClientId ClientId { get; set; }
    }

    public class ClientIdWithoutTypeConverterModel
    {
        public ClientIdWithoutTypeConverter ClientId { get; set; }
    }


    public class TypeConverter
    {
        [Test]
        public void ConvertToJsonWithoutTypeConverter()
        {
            var model = new ClientIdWithoutTypeConverterModel
            {
                ClientId = ClientIdWithoutTypeConverter.From("asdf12345")
            };

            var json = JsonConvert.SerializeObject(model);

            // This is usually not what we want when serializing ValueOf types.
            // We don't want the value to get wrapped inside the "Value" property when serilzing.
            // With the TypeConverter, this can be avoided.
            Assert.AreEqual("{\"ClientId\":{\"Value\":\"asdf12345\"}}", json);
        }

        [Test]
        public void ConvertToJsonWithTypeConverter()
        {
            var model = new ClientIdModel
            {
                ClientId = ClientId.From("asdf12345")
            };

            var json = JsonConvert.SerializeObject(model);
            Assert.AreEqual("{\"ClientId\":\"asdf12345\"}", json);
        }

        [Test]
        public void ConvertFromJsonWithoutTypeConverter()
        {
            var json = "{\"ClientId\":\"asdf12345\"}";

            // Without using a TypeConverter, the JSON serializer does not know, how to create the ValueOf type and throws an exception.
            var exception = Assert.Throws<JsonSerializationException>(() => JsonConvert.DeserializeObject<ClientIdWithoutTypeConverterModel>(json));
            Assert.AreEqual("Error converting value \"asdf12345\" to type 'ValueOf.Tests.ClientIdWithoutTypeConverter'. Path 'ClientId', line 1, position 23.", exception.Message);
        }

        [Test]
        public void ConvertFromJsonWithTypeConverter()
        {
            var json = "{\"ClientId\":\"asdf12345\"}";
            var model = JsonConvert.DeserializeObject<ClientIdModel>(json);

            Assert.AreEqual(ClientId.From("asdf12345"), model.ClientId);
        }
    }
}
