using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
namespace Lab4_OOAP_
{
    // Клас для представлення солодощів
    public class Sweet
    {
        // Властивість для зберігання назви солодощів
        public string Name { get; set; }

        // Властивість для зберігання ваги солодощів
        public double Weight { get; set; }

        // Конструктор, який ініціалізує назву і вагу солодощів
        public Sweet(string name, double weight)
        {
            Name = name;   // Призначаємо ім'я солодощів
            Weight = weight; // Призначаємо вагу солодощів
        }
    }

    // Клас для створення подарункових наборів
    public class GiftSet
    {
        // Властивість для зберігання назви подарункового набору
        public string Name { get; set; }

        // Властивість для зберігання ціни подарункового набору
        public double Price { get; set; }

        // Список солодощів, що входять до складу набору
        public List<Sweet> Sweets { get; set; } = new List<Sweet>();
    }

    // Інтерфейс для будівельника подарункових наборів
    public interface IGiftSetBuilder
    {
        // Метод для створення солодощів у наборі
        void BuildSweets();

        // Метод для отримання готового подарункового набору
        GiftSet GetGiftSet();
    }

    // Клас для створення економічного подарункового набору
    public class EconomicGiftSetBuilder : IGiftSetBuilder
    {
        // Змінна для зберігання подарункового набору
        private GiftSet giftSet = new GiftSet { Name = "Ласунка (Економічний)" };

        // Метод для створення солодощів
        public void BuildSweets()
        {
            // Додаємо солодощі до подарункового набору
            giftSet.Sweets.Add(new Sweet("Льодяники", 0.3));
            giftSet.Sweets.Add(new Sweet("Шоколадні цукерки", 0.4));
            giftSet.Sweets.Add(new Sweet("Вафлі", 0.2));
            giftSet.Sweets.Add(new Sweet("Драже", 0.1));
        }

        // Метод для отримання подарункового набору
        public GiftSet GetGiftSet() => giftSet;
    }

    // Клас для створення стандартного подарункового набору
    public class StandardGiftSetBuilder : IGiftSetBuilder
    {
        // Змінна для зберігання подарункового набору
        private GiftSet giftSet = new GiftSet { Name = "Наминайко (Стандартний)" };

        // Метод для створення солодощів
        public void BuildSweets()
        {
            // Додаємо солодощі до подарункового набору
            giftSet.Sweets.Add(new Sweet("Льодяники", 0.4));
            giftSet.Sweets.Add(new Sweet("Шоколадні цукерки", 0.3));
            giftSet.Sweets.Add(new Sweet("Вафлі", 0.2));
            giftSet.Sweets.Add(new Sweet("Драже", 0.1));
        }

        // Метод для отримання подарункового набору
        public GiftSet GetGiftSet() => giftSet;
    }

    // Клас для створення екстра подарункового набору
    public class ExtraGiftSetBuilder : IGiftSetBuilder
    {
        // Змінна для зберігання подарункового набору
        private GiftSet giftSet = new GiftSet { Name = "Пан Коцький (Екстра)" };

        // Метод для створення солодощів
        public void BuildSweets()
        {
            // Додаємо солодощі до подарункового набору
            giftSet.Sweets.Add(new Sweet("Льодяники", 0.3));
            giftSet.Sweets.Add(new Sweet("Шоколадні цукерки", 0.4));
            giftSet.Sweets.Add(new Sweet("Вафлі", 0.1));
            giftSet.Sweets.Add(new Sweet("Драже", 0.2));
        }

        // Метод для отримання подарункового набору
        public GiftSet GetGiftSet() => giftSet;
    }

    // Клас Директор для створення подарункових наборів
    public class GiftSetDirector
    {
        // Змінна для зберігання будівельника подарункового набору
        private IGiftSetBuilder builder;

        // Метод для встановлення будівельника
        public void SetBuilder(IGiftSetBuilder builder)
        {
            this.builder = builder; // Призначаємо будівельника
        }

        // Метод для конструкції подарункового набору
        public GiftSet Construct(double priceCandies, double priceChocolates, double priceWafers, double priceDragees)
        {
            // Викликаємо метод створення солодощів
            builder.BuildSweets();

            // Отримуємо готовий подарунковий набір
            GiftSet giftSet = builder.GetGiftSet();

            // Обчислюємо ціну подарункового набору
            giftSet.Price = CalculatePrice(giftSet, priceCandies, priceChocolates, priceWafers, priceDragees);

            // Повертаємо готовий подарунковий набір
            return giftSet;
        }

        // Метод для обчислення ціни подарункового набору
        private double CalculatePrice(GiftSet giftSet, double priceCandies, double priceChocolates, double priceWafers, double priceDragees)
        {
            double totalPrice = 0; // Ініціалізуємо загальну ціну

            // Проходимося по кожному солодощу в наборі
            foreach (var sweet in giftSet.Sweets)
            {
                // В залежності від назви солодощів, додаємо відповідну ціну до загальної
                switch (sweet.Name)
                {
                    case "Льодяники":
                        totalPrice += priceCandies * sweet.Weight; // Додаємо ціну льодяників
                        break;
                    case "Шоколадні цукерки":
                        totalPrice += priceChocolates * sweet.Weight; // Додаємо ціну шоколадних цукерок
                        break;
                    case "Вафлі":
                        totalPrice += priceWafers * sweet.Weight; // Додаємо ціну вафель
                        break;
                    case "Драже":
                        totalPrice += priceDragees * sweet.Weight; // Додаємо ціну драже
                        break;
                    default:
                        totalPrice += 0; // Якщо солодощі не відомі, нічого не додаємо
                        break;
                }
            }

            // Повертаємо загальну ціну
            return totalPrice;
        }
    }

    // Головна форма програми
    public partial class Form1 : Form
    {
        private TextBox txtPriceCandies;
        private TextBox txtPriceChocolates;
        private TextBox txtPriceWafers;
        private TextBox txtPriceDragees;

        public Form1()
        {
            InitializeComponent();
            InitializeControls();
        }

        private void InitializeControls()
        {
            // Поля для введення цін з Label
            Label lblPriceCandies = new Label { Text = "Ціна льодяників (грн/кг):", Location = new Point(20, 20), Size = new Size(200, 30) };
            Controls.Add(lblPriceCandies);
            txtPriceCandies = new TextBox { Location = new Point(220, 20), Size = new Size(100, 30) };
            Controls.Add(txtPriceCandies);

            Label lblPriceChocolates = new Label { Text = "Ціна шоколадних цукерок (грн/кг):", Location = new Point(20, 60), Size = new Size(200, 30) };
            Controls.Add(lblPriceChocolates);
            txtPriceChocolates = new TextBox { Location = new Point(220, 60), Size = new Size(100, 30) };
            Controls.Add(txtPriceChocolates);

            Label lblPriceWafers = new Label { Text = "Ціна вафель (грн/кг):", Location = new Point(20, 100), Size = new Size(200, 30) };
            Controls.Add(lblPriceWafers);
            txtPriceWafers = new TextBox { Location = new Point(220, 100), Size = new Size(100, 30) };
            Controls.Add(txtPriceWafers);

            Label lblPriceDragees = new Label { Text = "Ціна драже (грн/кг):", Location = new Point(20, 140), Size = new Size(200, 30) };
            Controls.Add(lblPriceDragees);
            txtPriceDragees = new TextBox { Location = new Point(220, 140), Size = new Size(100, 30) };
            Controls.Add(txtPriceDragees);

            // Кнопка для створення подарункового набору
            Button buttonCreate = new Button
            {
                Text = "Створити набір",
                Location = new Point(20, 180),
                Size = new Size(150, 30)
            };
            buttonCreate.Click += ButtonCreate_Click;
            Controls.Add(buttonCreate);

            // Radio buttons для вибору подарункового набору
            RadioButton rbtnEconomic = new RadioButton
            {
                Text = "Ласунка (Економічний)",
                Location = new Point(350, 20),
                Size = new Size(200, 30)
            };
            Controls.Add(rbtnEconomic);

            RadioButton rbtnStandard = new RadioButton
            {
                Text = "Наминайко (Стандартний)",
                Location = new Point(350, 60),
                Size = new Size(200, 30)
            };
            Controls.Add(rbtnStandard);

            RadioButton rbtnExtra = new RadioButton
            {
                Text = "Пан Коцький (Екстра)",
                Location = new Point(350, 100),
                Size = new Size(200, 30)
            };
            Controls.Add(rbtnExtra);
        }

        private void ButtonCreate_Click(object sender, EventArgs e)
        {
            // Перевірка коректності введених цін
            // Спробуємо перетворити текстові значення з текстових полів на числові (тип double).
            // Якщо перетворення не вдається, виводимо повідомлення про помилку та виходимо з методу.
            if (!double.TryParse(txtPriceCandies.Text, out double priceCandies) ||
                !double.TryParse(txtPriceChocolates.Text, out double priceChocolates) ||
                !double.TryParse(txtPriceWafers.Text, out double priceWafers) ||
                !double.TryParse(txtPriceDragees.Text, out double priceDragees))
            {
                MessageBox.Show("Будь ласка, введіть коректні числові значення для цін.", "Помилка");
                return; // Виходимо з методу, якщо введення некоректне.
            }

            // Створення об'єкта директора, який відповідає за будівництво подарункових наборів.
            GiftSetDirector director = new GiftSetDirector();
            IGiftSetBuilder builder;

            // Перевірка, який з радіо-кнопок обрано, та вибір відповідного будівельника.
            if (Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked) is RadioButton checkedRadio)
            {
                switch (checkedRadio.Text)
                {
                    case "Ласунка (Економічний)":
                        // Якщо вибрано "Ласунка (Економічний)", створюємо будівельника економічного набору.
                        builder = new EconomicGiftSetBuilder();
                        break;
                    case "Наминайко (Стандартний)":
                        // Якщо вибрано "Наминайко (Стандартний)", створюємо стандартного будівельника.
                        builder = new StandardGiftSetBuilder();
                        break;
                    case "Пан Коцький (Екстра)":
                        // Якщо вибрано "Пан Коцький (Екстра)", створюємо екстра-будівельника.
                        builder = new ExtraGiftSetBuilder();
                        break;
                    default:
                        // Якщо вибір некоректний, викликаємо виняток.
                        throw new InvalidOperationException("Неправильний вибір.");
                }
            }
            else
            {
                // Якщо жоден тип подарункового набору не вибрано, виводимо повідомлення про помилку.
                MessageBox.Show("Будь ласка, виберіть тип подарункового набору.", "Помилка");
                return; // Виходимо з методу.
            }

            // Встановлюємо обраного будівельника в директорі.
            director.SetBuilder(builder);
            // Будуємо подарунковий набір з введеними цінами.
            GiftSet giftSet = director.Construct(priceCandies, priceChocolates, priceWafers, priceDragees);

            // Підготовка рядка для виведення деталей про солодощі в подарунковому наборі.
            string sweetsDetails = "";
            foreach (var sweet in giftSet.Sweets)
            {
                sweetsDetails += $"{sweet.Name}: {sweet.Weight} кг\n"; // Додаємо інформацію про кожну солодкість.
            }

            // Формуємо фінальне повідомлення про створений подарунковий набір.
            string message = $"Створено {giftSet.Name}:\nЦіна: {giftSet.Price} грн\nВага солодощів:\n{sweetsDetails}";

            // Виводимо інформацію про успішне створення подарункового набору.
            MessageBox.Show(message, "Успішно");
        }
    }
}