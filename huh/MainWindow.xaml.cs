using Microsoft.Win32;
using System.Diagnostics;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;

namespace huh
{
    public partial class MainWindow : Window
    {
        Graph graph = new Graph();
        public string integ;
        List<GraphField> graphs = new List<GraphField>();

        public MainWindow()
        {
            InitializeComponent();
        }

        //private void btnStart_Click(object sender, RoutedEventArgs e)
        //{
        //    FirstSP.Visibility = Visibility.Collapsed;
        //    spSettingLabel.Visibility = Visibility.Visible;
        //}

        private void btnPChart_Click(object sender, RoutedEventArgs e)
        {
            spExport.Visibility = Visibility.Visible;
            graph.graphType = "PieChart";
        }

        private void btnHGrahp_Click(object sender, RoutedEventArgs e)
        {
            spExport.Visibility = Visibility.Visible;
            graph.graphType = "HGraph";
        }

        private void btnVGraph_Click(object sender, RoutedEventArgs e)
        {
            spExport.Visibility = Visibility.Visible;
            graph.graphType = "VGraph";
        }
        private void btnPolarChart_Click(object sender, RoutedEventArgs e)
        {
            spExport.Visibility = Visibility.Visible;
            graph.graphType = "PolarChart";
        }

        private void btnSChart_Click(object sender, RoutedEventArgs e)
        {
            spExport.Visibility = Visibility.Visible;
            graph.graphType = "SChart";
        }

        private void btnTyping_Click(object sender, RoutedEventArgs e)
        {
            spManualInput.Visibility = Visibility.Visible;
            spTypyOfDiagram.Visibility = Visibility.Collapsed;
            spExport.Visibility = Visibility.Collapsed;
        }

        private void btnExcel_Click(object sender, RoutedEventArgs e)
        {
            
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            bool? success = openFileDialog.ShowDialog();
            var path = openFileDialog.FileName;
            if (success == true) 
            {
                foreach (var item in path)
                {
                    
                    
                }
            }
            else { MessageBox.Show("File didnt choose", "MESSAGE", MessageBoxButton.OK, MessageBoxImage.Information); }
        }

        private void btnJsonf_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            JsonImport jsonImport = new JsonImport();
            openFileDialog.Filter = "Json files (*.json)|*.json|Text files (*.txt)|*.txt";
            bool? success = openFileDialog.ShowDialog();
            if (success == true)
            {
                jsonImport.path = openFileDialog.FileName;
                jsonImport.JI(out ViewGraph graphj);
                switch (graph.graphType)
                {
                    case "PieChart":
                        spPieChart.Visibility = Visibility.Visible;
                        this.DataContext = graphj;
                        break;

                    case "VGraph":
                        spVChart.Visibility = Visibility.Visible;
                        this.DataContext = graphj;
                        break;

                    case "HGraph":
                        spHChart.Visibility = Visibility.Visible;
                        this.DataContext = graphj;
                        break;
                    case "PolarChart":
                        spPolarChart.Visibility = Visibility.Visible;
                        this.DataContext = graphj;
                        break;
                    case "SChart":
                        spSChart.Visibility = Visibility.Visible;
                        this.DataContext = graphj;
                        break;
                }
            }
            else { MessageBox.Show("File didnt choose", "MESSAGE", MessageBoxButton.OK, MessageBoxImage.Information); }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            foreach (var stack in spValue.Children)                                 //пробежка по всем элементам окна
            {
                StackPanel? stackPanel = stack as StackPanel;                       //поиск StackPanel
                if (stackPanel != null)
                {
                    foreach (var tbn in stackPanel.Children)                        //пробежка по всем полям
                    {
                        GraphField graphField = new GraphField();
                        if (tbn != null)
                        {
                            TextBox? textBox = tbn as TextBox;
                            if (textBox != null && textBox.Name == "tbGraphName")   //заполнение имени
                            {
                                graphField.graphName = textBox.Text;
                            }
                            if (textBox != null && textBox.Name == "tbGraphValue")  //заполнение величины
                            {
                                if (Int32.TryParse(textBox.Text, out var val)) graphField.graphValue = val;
                                else graphField.graphValue = 0;
                            }
                        }
                        graphs.Add(graphField);                                     //добавление графа
                    }
                }
            }
            //МАГИЧЕСКАЯ КНОПКА КРАФТИТ ДИАГРАММУ
            ViewGraph vgraph = new ViewGraph(graphs);
            switch (graph.graphType)
            {
                case "PieChart":
                    spPieChart.Visibility = Visibility.Visible;
                    this.DataContext = vgraph;
                    break;

                case "VGraph":
                    spVChart.Visibility = Visibility.Visible;
                    this.DataContext = vgraph;
                    break;

                case "HGraph":
                    spHChart.Visibility = Visibility.Visible;
                    this.DataContext = vgraph;
                    break;
                case "PolarChart":
                    spPolarChart.Visibility = Visibility.Visible; 
                    this.DataContext = vgraph;
                    break;
                case "SChart":
                    spSChart.Visibility = Visibility.Visible;
                    this.DataContext = vgraph;
                    break;
            }
        }
        bool click = false;
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (!click)
            { 
                spValue.Visibility = Visibility.Visible;
                spRefreash.Visibility = Visibility.Visible;
                FieldsCreater();
                click = true;
            }
        }
        private void message(String mes)    //Упрощаем вызов сообщений
        {
            MessageBox.Show(mes, "MESSAGE", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void createField()
        {
            //это стек нейм
            StackPanel spPanelN = getPanel("Name");
            //это стек вэлью
            StackPanel spPanelV = getPanel("Value");

            spValue.Children.Add(spPanelN);
            spValue.Children.Add(spPanelV);
        }
        private StackPanel getPanel(string st)
        {
            StackPanel local = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                Name = "spForTwoFields"
            };
            local.Children.Add(getLabel(st));
            local.Children.Add(getBox(st));
            return local;
        }
        private Label getLabel(String str)
        {
            return new Label() { Content = "Enter " + str + " of graph:" };
        }
        private TextBox getBox(String str)
        {
            return new TextBox() { Name = "tbGraph" + str };
        }

        public void FieldsCreater()
        {
            String tbText = tbFields.Text;
            Graph graphField = new Graph();
            int counter;
            if (String.IsNullOrEmpty(tbText)) message("Enter something.");
            else
            {
                if (int.TryParse(tbText, out counter))
                {
                    graphField.fieldQuantity = int.Parse(tbText);
                    for (int i = 0; i < graphField.fieldQuantity; i++)
                    {
                        createField();
                    }
                }
                else message("Not an integer.");
            }
            spBtnCreate.Visibility = Visibility.Visible;
        }
              
        private void btnRefreash_Click(object sender, RoutedEventArgs e)
        {
            //graph.fieldQuantity = 0;
            //tbFields.Clear();
            //graphs.Clear();
            //FieldsCreater();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();//лицензия <3

        }
    }
}