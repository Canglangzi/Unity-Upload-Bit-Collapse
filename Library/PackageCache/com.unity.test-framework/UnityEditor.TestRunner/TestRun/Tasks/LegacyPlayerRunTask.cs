using System;
using System.Collections;
using System.Linq;
using UnityEditor.TestRunner.TestLaunchers;
using UnityEngine.TestTools.TestRunner;

namespace UnityEditor.TestTools.TestRunner.TestRun.Tasks
{
    internal class LegacyPlayerRunTask : TestTaskBase
    {
        public LegacyPlayerRunTask()
        {
            SupportsResumingEnumerator = true;
        }
        public override IEnumerator Execute(TestJobData testJobData)
        {
            var executionSettings = testJobData.executionSettings;
            var launcher = new PlayerLauncher(testJobData.PlayModeSettings, executionSettings.targetPlatform, executionSettings.overloadTestRunSettings, executionSettings.playerHeartbeatTimeout, executionSettings.playerSavePath, testJobData.InitTestScenePath, testJobData.InitTestScene, testJobData.PlaymodeTestsController);
            launcher.Run();
            testJobData.PlayerBuildOptions = launcher.playerBuildOptions.BuildPlayerOptions; // This can be removed once the player build options are created in a separate task

            yield return null;
        }
    }
}
