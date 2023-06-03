using System.Runtime.Serialization;

namespace Ordering.Application.Exceptions
{
    [Serializable]
    public class NotFoundException : ApplicationException, ISerializable
    {
        public string EntityName { get; private set; }
        public object Key { get; private set; }

        public NotFoundException(string name, object key)
            : base($"Entity \"{name}\" ({key}) was not found.")
        {
            EntityName = name;
            Key = key;
        }

        // Serialization constructor
        protected NotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            // Deserialize the specific properties of the exception
            EntityName = info.GetString("EntityName");
            Key = info.GetValue("Key", typeof(object));
        }

        // GetObjectData method for serialization
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            // Add the specific properties of the exception for serialization
            info.AddValue("EntityName", EntityName);
            info.AddValue("Key", Key, typeof(object));
        }
    }
}
