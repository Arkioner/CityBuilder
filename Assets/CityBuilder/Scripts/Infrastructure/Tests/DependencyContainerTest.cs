using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace CityBuilder.Scripts.Infrastructure.Tests
{
    public class DependencyContainerTest
    {
        private DependencyContainer _dependencyContainer;

        [SetUp]
        public void SetUp()
        {
            _dependencyContainer = DependencyContainer.GetInstance();
        }

        [Test]
        public void DependencyContainerTestCallingUnityControllerWorks()
        {
            // Use the Assert class to test conditions.
            UnityController unityController = _dependencyContainer.GetUnityController();
            Assert.That(unityController, Is.Not.Null);
        }

        [Test]
        public void DependencyContainerTestCallingUnityControllerTwiceWorks()
        {
            // Use the Assert class to test conditions.
            UnityController unityController = _dependencyContainer.GetUnityController();
            Assert.That(unityController, Is.Not.Null);
            UnityController unityControllerAgain = _dependencyContainer.GetUnityController();
            Assert.That(unityController, Is.SameAs(unityControllerAgain));
        }

        // A UnityTest behaves like a coroutine in PlayMode
        // and allows you to yield null to skip a frame in EditMode
        [UnityTest]
        public IEnumerator DependencyContainerPlayModeTestWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // yield to skip a frame
            yield return null;
        }
    }
}