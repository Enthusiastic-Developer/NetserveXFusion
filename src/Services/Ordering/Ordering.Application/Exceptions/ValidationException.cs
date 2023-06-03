using FluentValidation.Results;
using System.Runtime.Serialization;

namespace Ordering.Application.Exceptions
{
    [Serializable]
    public class ValidationException : ApplicationException, ISerializable
    {
        public IDictionary<string, string[]> Errors { get; }

        public ValidationException()
            : base("One or more validation failures have occurred.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }

        // Serialization constructor
        protected ValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            // Deserialize the Errors property
            Errors = (Dictionary<string, string[]>)info.GetValue("Errors", typeof(Dictionary<string, string[]>));
        }

        // GetObjectData method for serialization
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            // Serialize the Errors property
            info.AddValue("Errors", Errors, typeof(Dictionary<string, string[]>));
        }
    }
}
