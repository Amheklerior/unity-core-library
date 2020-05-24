
namespace Amheklerior.Core.Test.Utilities {

    internal class TestObject {
        private static int ID = 1;

        public int Id { get; private set; }

        public TestObject() {
            Id = ID;
            ID++;
        }

        public override bool Equals(object obj) => obj is TestObject castedObj ? Id == castedObj.Id : false;
        public override int GetHashCode() => Id;
    }

}
