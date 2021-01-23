using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using System.Xml.Linq;

namespace Project_Nanotec
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        XmlDocument doc = new XmlDocument();
        string path = "";

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenfileDialog();

        }



        private void OpenfileDialog()
        {
            OpenFileDialog open = new OpenFileDialog()
            {
                Title = "Open",
                Multiselect = true,
                Filter = "XML File (.xml)|*.xml|All Files (*.*)|*.*",
                FileName = ""

            };

            bool? result = open.ShowDialog();

            if (result == true)
            {
                try
                {
                    doc.Load(open.FileName);
                    treeview.Items.Clear();

                    BuildTree(treeview, XDocument.Load(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), open.FileName)));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Following error has occured: " + ex);
                }
            }
            lb1.Content = open.FileName;
            path = open.FileName;

        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            doc.Save(path);
        }


        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            SaveFileDialog();
        }

        private void SaveFileDialog()
        {
            SaveFileDialog save = new SaveFileDialog()
            {
                Title = "Save",
                Filter = "XML File (.xml)|*.xml|All Files (*.*)|*.*",
                FileName = ""
            };

           

            if(save.ShowDialog().Value)
            {
                doc.Save(save.FileName);
            }

        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }


        private void BuildTree(TreeView treeview,XDocument doc)
        {
            TreeViewItem tree = new TreeViewItem
            {
                Header = doc.Root.Name.LocalName,
                IsExpanded = true

            };
            treeview.Items.Add(tree);
            BuildNodes(tree, doc.Root);
        }
       
        private void BuildNodes (TreeViewItem item,XElement element)
        {
            TreeViewItem node = new TreeViewItem
            {
               Header = element.Name.LocalName,
               IsExpanded = true
            };

            if(element.HasElements)
            {
                foreach (XElement child in element.Elements())
                {
                    BuildNodes(node, child);
                }
            }
            else
            {
                TreeViewItem childNodes = new TreeViewItem
                {
                    Header = element.Value,
                    IsEnabled = true
                };
                node.Items.Add(childNodes);
            }
            item.Items.Add(node);

        }

        private void EditBt_Click(object sender, RoutedEventArgs e)
        {
            if (treeview.Items.IsEmpty == false )
            {
                tb1.Visibility = Visibility.Visible;
                tb2.Visibility = Visibility.Visible;
                tb3.Visibility = Visibility.Visible;
                Tlb.Visibility = Visibility.Visible;
                Alb.Visibility = Visibility.Visible;
                Ylb.Visibility = Visibility.Visible;
                ApplyBT.Visibility = Visibility.Visible;
                txBlock.Visibility = Visibility.Visible;
            }
            else
            {
                lb2.Visibility = Visibility.Visible;
            }

        }

        private void SelectedItemChanged(object sender,RoutedPropertyChangedEventArgs<Object> e)
        {
            tb1.Text = ((TreeViewItem)e.NewValue).Header.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            XmlNodeList list = doc.SelectNodes("/Bookstore/Book");

            foreach (XmlNode node in list)
            {
                node["Title"].InnerText = tb1.Text;
                node["Author"].InnerText = tb2.Text;
                node["Year"].InnerText = tb3.Text;

            }
        }



    }
}
