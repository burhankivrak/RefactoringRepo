using MongoDB.Bson;

namespace ConsoleAppSquareMaster
{
    public class EmpireModel
    {
        public ObjectId Id { get; set; }
        public int Name { get; set; }
        public int Size { get; set; }
        public double Percentage { get; set; }
        public int AlgorithmType { get; set; }

        public ObjectId WorldId { get; set; }
    }
}
