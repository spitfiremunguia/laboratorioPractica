﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace laboratorioPractica
{
    public class myTabControl:TabControl
    {
        private Point lastClickPos;
        private ContextMenuStrip cms;//context menu strip
        public myTabControl()
        {
            cms = getcms();
        }
        private ContextMenuStrip getcms()
        {
            ContextMenuStrip cms = new ContextMenuStrip();
            cms.Items.Add("Close", laboratorioPractica.Properties.Resources.icons8_Cerrar_ventana_48, new EventHandler(this.Item_Clicked));
            return cms;
        }
        private void Item_Clicked(object sender, EventArgs e)
        {
            for (int i = 0; i < this.TabCount; i++)
            {

                Rectangle rect = this.GetTabRect(i);
                if (rect.Contains(this.PointToClient(lastClickPos)))
                {
                    if (TabPages[i].Text != "Main")
                    {
                        this.TabPages.RemoveAt(i);
                    }

                }
            }
        }
        //calcula la posición actual del cursor y decide si mostrar el menu
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (e.Button == MouseButtons.Right)
            {
                lastClickPos = Cursor.Position;
                cms.Show(Cursor.Position);
            }
        }
    }
}
