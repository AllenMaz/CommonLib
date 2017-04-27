using AModel;
using AUtility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLibUnitTest
{
    [TestClass]
    public class ObjectConvertTest
    {
        [TestMethod()]
        public void ConvertTest()
        {

            ModelClass model = new ModelClass()
            {
                id = "234234234sdfsdfsdfsdfsdfsdf",
                number = "23",
                Name = "allen",
                IsRight = "true",
                IsWrong = "false",
                Date = "2017-09-09",
                CreateDate = "2017-12-12",
                childs = new List<ModelClassChild>() { 
                   new ModelClassChild(){
                    id= "1111",
                    childname ="child1"
                   },
                    new ModelClassChild(){
                    id= "2222",
                    childname ="child2"
                   }
                }
            };

            var convertSuccess = true;
            try
            {
                EntityClass entity = new EntityClass();
                entity = ObjectConvert.Convert<ModelClass, EntityClass>(model);


                ModelClass cM = ObjectConvert.Convert<EntityClass, ModelClass>(entity);

            }
            catch (Exception e)
            {
                convertSuccess = false;
            }


            Assert.IsTrue(convertSuccess);
        }
    }

    public class ModelClass : BaseModel
    {
        public string id { get; set; }

        public string number { get; set; }

        public string Name { get; set; }

        public string IsRight { get; set; }

        public string IsWrong { get; set; }

        public string Date { get; set; }

        public string CreateDate { get; set; }

        public List<ModelClassChild> childs { get; set; }

        public ModelClass()
        {
            childs = new List<ModelClassChild>();
        }

    }

    public class ModelClassChild
    {

        public string id { get; set; }

        public string childname { get; set; }



    }

    public class EntityClass
    {
        public string id { get; set; }

        public Int32 number { get; set; }

        public string Name { get; set; }

        public bool IsRight { get; set; }

        public bool IsWrong { get; set; }

        public DateTime Date { get; set; }

        public DateTime? CreateDate { get; set; }

        public List<EntityClassChild> childs { get; set; }

        public EntityClass()
        {
            childs = new List<EntityClassChild>();
        }
    }

    public class EntityClassChild
    {

        public int id { get; set; }

        public string childname { get; set; }
    }
}
