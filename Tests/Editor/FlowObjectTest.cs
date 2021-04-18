using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class FlowObjectTest
    {
        FlowObject flowObject;
        GameObject gameObject;

        [SetUp]
        public void Init() {
            gameObject = new GameObject();
            flowObject = new FlowObject { transform = gameObject.transform };
        }

        [Test]
        public void InitResetsCorrectly()
        {

            var startPosition = Vector3.zero;
            var target = Vector3.forward;
            var curveX = new AnimationCurve();
            var curveY = new AnimationCurve();
            var speed = 1f;

            flowObject.Init(startPosition, target, speed, curveX, curveY);

            Assert.AreEqual(flowObject.startPosition, startPosition);
            Assert.AreEqual(flowObject.target, target);
            Assert.AreEqual(flowObject.speed, speed);
            Assert.AreEqual(flowObject.curveX, curveX);
            Assert.AreEqual(flowObject.curveY, curveY);
            Assert.AreEqual(flowObject.startTime, 0f);
            Assert.AreEqual(flowObject.direction, target - flowObject.transform.position);
        }

        [Test]
        public void SetActiveChangesGameObjectState()
        {
            flowObject.SetActive(false);
            Assert.IsFalse(flowObject.transform.gameObject.activeSelf);
            
            flowObject.SetActive(true);
            Assert.IsTrue(flowObject.transform.gameObject.activeSelf);
        }
    }
}
