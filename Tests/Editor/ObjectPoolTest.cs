using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ObjectPoolTest
    {

        ObjectPool<object> objectPool;

        /// <summary>
        /// ObjectGenerator helps objectPool to create new objects on demand.
        /// </summary>
        /// <returns>A dummy object</returns>
        object ObjectGenerator() {
            return new object();
        }

        [SetUp]
        public void Init() {
            objectPool = new ObjectPool<object>(ObjectGenerator);
        }

        [Test]
        public void GetCreatesANewObject()
        {
            var newObject = objectPool.Get();
            Assert.IsNotNull(newObject);
        }

        [Test]
        public void ReturnPutsObjectInQueue()
        {   
            var existingObject = new object();
            objectPool.Return(existingObject);
            
            var newObject = objectPool.Get();
            Assert.AreEqual(newObject, existingObject);
        }
    }
}
