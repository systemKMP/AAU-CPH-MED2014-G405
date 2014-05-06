public class Result
{

    public float xOffset;
    public float yOffset;
    public float totalOffset;

    public Result()
    {
        xOffset = 0.0f;
        yOffset = 0.0f;
        totalOffset = 0.0f;
    }

    public Result(float x, float y, float total)
    {
        xOffset = x;
        yOffset = y;
        totalOffset = total;
    }
}
