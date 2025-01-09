using System.Diagnostics.CodeAnalysis;

namespace ExampleExam.Tests
{
    [TestClass()]
    [ExcludeFromCodeCoverage]
    public class PlayGroundsDBTests
    {
        private static PlayGroundsDB _repo = new PlayGroundsDB();
        [TestInitialize]
        public void Setup()
        {
            _repo.Nuke();
            _repo.Setup();
        }

        [TestMethod()]
        public void PlayGroundsDBTest()
        {
            Assert.IsNotNull(_repo);
        }

        [TestMethod()]
        [DataRow(5, "TEST", 15, 15)]
        public void AddTest(int id, string name, int maxchildren, int minage)
        {
            int expected = 3;
            PlayGround pgr = _repo.Add(new PlayGround(id, name, maxchildren, minage));
            int actual = pgr.Id;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetAllTest()
        {
            List<PlayGround> pgr = _repo.GetAll();
            Assert.IsTrue(2 == pgr.Count);
        }

        [TestMethod()]
        [DataRow(1)]
        public void GetByIdTest(int id)
        {
            PlayGround actual = _repo.GetById(id);
            Assert.IsTrue("TEST1" == actual.Name && 10 == actual.MaxChildren && 10 == actual.MinAge);
        }

        [TestMethod()]
        [DataRow(1, "UPDATED", 50, 50)]
        [DataRow(2, "CHANGED", 100, 100)]
        public void UpdateTest(int id, string name, int maxchildren, int minage)
        {
            PlayGround expected = new PlayGround(id, name, maxchildren, minage);
            PlayGround actual = _repo.Update(id, expected);
            Assert.AreEqual(name, actual.Name);
        }

        [TestMethod()]
        public void NukeTest()
        {
            _repo.Nuke();
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void SetupTest()
        {
            _repo.Setup();
            Assert.IsTrue(true);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _repo.Nuke();
        }
    }
}