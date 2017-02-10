namespace unittests.Serialization
{
    using System.Collections;
    using MessageHandler.Runtime.Serialization;
    using Xunit;

    public class When_deserializing_primitive_types
    {
        public static IEnumerable PrimitivePermutations
        {
            get
            {
                yield return new object[] { "2147483647", int.MaxValue };
                yield return new object[] { "9223372036854775807", long.MaxValue };
                yield return new object[] { "true", true };
                yield return new object[] { "1.2E+0", 1.2 };
                yield return new object[] { "1.2", 1.2m };
            }
        }


        [Theory]
        [MemberData("PrimitivePermutations")]
        public void Can_deserialize_to_proper_primitive_types(string serializedValue, object expected)
        {
            dynamic deserialized = Json.Decode(serializedValue);

            Assert.Equal(expected, deserialized);
        }

    }
}