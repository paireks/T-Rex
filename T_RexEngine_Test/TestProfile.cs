using T_RexEngine;
using Xunit;

namespace T_RexEngine_Test
{
    [Collection("Rhino Collection")]
    public class TestProfile
    {
        [Fact]
        public void TestToString()
        {
            Profile profile = new Profile("Name", 10.0, 20.0, 0.001);
            
            Assert.Equal("Profile\r\nName: Name", profile.ToString());
        }
    }
}