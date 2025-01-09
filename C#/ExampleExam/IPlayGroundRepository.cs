
namespace ExampleExam
{
    public interface IPlayGroundRepository
    {
        PlayGround Add(PlayGround pgr);
        List<PlayGround> GetAll();
        PlayGround GetById(int id);
        PlayGround Update(int id, PlayGround pgr);
    }
}