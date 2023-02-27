namespace kb21_web.Servicec
{
    public interface ITestObject
    {
        string Name { get; }
        void Write(string value);
    }

    public class TestObject:ITestObject
    { 
        public TestObject() 
        { 
            Name= Guid.NewGuid().ToString();
        }

        public string Name { get; set; }
        public void Write(string value) 
        { 

        }
    }
}
