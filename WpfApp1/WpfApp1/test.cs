using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace WpfApp1
{
        [TestFixture]
        class tasteCase{

        [TestCase]
        public void isBroken() {
            CGenerator gen = new CGenerator();

            gen.field = new int[,] { 
                { -1, -1,  0,  0,  0 },
                { -1, -1, -1, -1,  0 },
                {  0, -1, -1, -1,  0 },
                {  0, -1, -1, -1,  0 } };

            Assert.AreEqual(true, gen.isBroken(0, 0));
            Assert.AreEqual(false, gen.isBroken(1, 0));
            Assert.AreEqual(true, gen.isBroken(2, 2));

            var ex = Assert.Throws<ArgumentException>(() => gen.isBroken(-1, 15));
            Assert.That(ex.Message, Is.EqualTo("ВЫХОД ЗА ГРАНИЦУ"));
            var ex1 = Assert.Throws<ArgumentException>(() => gen.isBroken(-1, 15));
            Assert.That(ex1.Message, Is.EqualTo("ВЫХОД ЗА ГРАНИЦУ"));

        }

        [TestCase]
        public void plantMines()
        {
            CGenerator gen = new CGenerator();

            gen.field = new int[5,5] ;

            gen.plantMines(10);

            int mines = 0;

            for (int i = 0; i < gen.field.GetLength(0); i++)
                for (int j = 0; j < gen.field.GetLength(1); j++)
                    if (gen.field[i, j] == -1)
                        mines++;

            Assert.AreEqual(10, mines);

            bool isBroken = false;

            for (int i = 0; i < gen.field.GetLength(0); i++)
                for (int j = 0; j < gen.field.GetLength(1); j++)
                    if (gen.isBroken(i, j) == true)
                        isBroken = true;

            Assert.AreEqual(false, isBroken);

            var ex2 = Assert.Throws<ArgumentException>(() => gen.plantMines(20));
            Assert.That(ex2.Message, Is.EqualTo("МНОГО МИН"));

            var ex3 = Assert.Throws<ArgumentException>(() => gen.plantMines(2));
            Assert.That(ex3.Message, Is.EqualTo("МАЛО МИН"));
        }

        [TestCase]
        public void reveal()
        {
            CGenerator gen = new CGenerator();

            gen.field = new int[,] {
                {  3, -1,  2,  0,  0 },
                { -1, -1,  2,  0,  0 },
                {  2,  2,  2,  1,  1 },
                {  0,  0,  1, -1,  1 } };

            gen.reveal(3, 1);
            gen.reveal(0, 3);
            Assert.AreEqual(10, gen.field[3, 1]);
            Assert.AreEqual(10, gen.field[0, 3]);
            Assert.AreEqual(10, gen.field[3, 0]);
            Assert.AreEqual(2, gen.field[1, 2]);
        }


        [TestCase]
        public void calculate()
        {
            CGenerator gen = new CGenerator();

            gen.field = new int[,] 
            {
                {  0,  0,  0, -1,  0 },
                {  0, -1,  0, -1,  0 },
                {  0, -1, -1,  0,  0 },
                {  0, -1,  0, -1,  0 }
            };
            gen.calculate();
            Assert.AreEqual(1, gen.field[0, 0]);
            Assert.AreEqual(4, gen.field[3, 2]);
            Assert.AreEqual(5, gen.field[1, 2]);
            Assert.AreEqual(1, gen.field[3, 4]);
            Assert.AreEqual(3, gen.field[2, 3]);

        }

    }

}