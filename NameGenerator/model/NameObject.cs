
namespace NameGenerator.model
{
    class NameObject
    {
        public string language;
        public string gender;
        public string name;

        public NameObject(string language, string gender, string name)
        {
            this.language = language;
            this.gender = gender;
            this.name = name;
        }

        public override string ToString()
        {
            return language + " " + gender + " " + name;
        }

        public override bool Equals(object obj)
        {
            // if it's a NameObject, check the fields for equality, otherwise use the regular Equals() method
            if (obj.GetType().Equals(typeof(NameObject)))
            {
                return this.language == (obj as NameObject).language && this.gender == (obj as NameObject).gender && this.name == (obj as NameObject).name;
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
