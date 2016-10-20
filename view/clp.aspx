﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="clp.aspx.cs" Inherits="PR3.view.pr3" EnableEventValidation="false" %>

<%@ Register TagPrefix="ce" Namespace="Grids" Assembly="ControlExtensions" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script src="../library/js/jquery-1.12.4.js"></script>
    <script src="../library/js/jquery-ui.js"></script>
    <script src="/library/js/jquery.Jcrop.js"></script>
    <script src="/library/js/Search.js"></script>

    <link href="../library/css/style.css" rel="stylesheet" />
    <link href="../library/css/style.css" rel="stylesheet" />
    <link href="/library/css/jquery.Jcrop.css" rel="stylesheet" />
    <link href="../library/css/bootstrap-theme.css" rel="stylesheet" />
    <link href="../library/css/bootstrap-theme.min.css" rel="stylesheet" />
    <link href="../library/css/bootstrap.css" rel="stylesheet" />
    <link href="../library/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../library/css/jquery-ui.css" rel="Stylesheet" />

<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>

    <script>
        $(function () {
            $("#cropimg").draggable();
              $("#imgUpload").Jcrop({
                  onSelect: croparea
              });
          });

          function croparea(c) {
              $("#X").val(c.x);
              $("#Y").val(c.y);
              $("#W").val(c.w);
              $("#H").val(c.h);
          }

          var currFFZoom = 1;
          var currIEZoom = 100;

          function plus() {
              //alert('sad');
              var step = 0.2;
              currFFZoom += step;
              $("#cropimg").css('MozTransform', 'scale(' + currFFZoom + ')');
              var stepie = 2;
              currIEZoom += stepie;
              $("#cropimg").css('zoom', ' ' + currIEZoom + '%');

          };
          function minus() {
              //alert('sad');
              var step = 0.2;
              currFFZoom -= step;
              $("#cropimg").css('MozTransform', 'scale(' + currFFZoom + ')');
              var stepie = 2;
              currIEZoom -= stepie;
              $("#cropimg").css('zoom', ' ' + currIEZoom + '%');
          };
      </script>

    <style type="text/css">
        .auto-style23 {
            height: 36px;
        }
    </style>

</head>
<body>
  <form id="form1" runat="server">
    <nav class="navbar navbar-inverse navbar-fixed-top">
            <div class="container">
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target=".navbar-collapse">
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                     <a class="navbar-brand" href="#"> PR3-SMART CALEPINAGE 
                         <asp:Panel ID="Panel5" runat="server" Height="30px" style="margin-left: 989px">
                            <asp:ImageButton ID="ImageButton2"  ImageUrl="~/Images/deconnexion.png" style="height:48px; margin-left: 100%; margin-top: -26px" type="image" runat="server" OnClick="ImageButton2_Click" />
                         </asp:Panel>              
                    </a>
                &nbsp;
                </div>
                <div class="collapse navbar-collapse"></div>
            </div>
     </nav>
    <br/><br/><br/> 

    <asp:Panel ID="Panel3" runat="server" style="margin-left: 25%" Width="657px">
        <table class="auto-style1">
            <tr>
                <td class="auto-style3">
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/img/crayon.png" Width="22px" />
                </td>
                <td class="auto-style3">
                    <asp:Image ID="Image3" runat="server" ImageUrl="~/img/couleur font.png" Width="26px" />
                </td>
                <td class="auto-style3">
                    <asp:Image ID="Image4" runat="server" ImageUrl="~/img/selecteur texte.png" Width="24px" />
                </td>
                <td class="auto-style3">
                    <asp:Image ID="Image5" runat="server" ImageUrl="~/img/noir.png" Height="24px" />
                </td>
                <td class="auto-style3">
                    <asp:Image ID="Image6" runat="server" ImageUrl="~/img/Gris.png" Height="25px" />
                </td>
                <td class="auto-style3">
                    <asp:Image ID="Image7" runat="server" ImageUrl="~/img/grenat.png" Height="27px" />
                </td>
                <td class="auto-style3">
                    <asp:Image ID="Image8" runat="server" ImageUrl="~/img/rouge.png" Height="25px" />
                </td>
                <td class="auto-style3">
                    <asp:Image ID="Image14" runat="server" ImageUrl="~/img/orange.png" Height="26px" />
                </td>
                <td class="auto-style3">
                    <asp:Image ID="Image10" runat="server" ImageUrl="~/img/jaune.png" Height="27px" />
                </td>
                <td class="auto-style3">
                    <asp:Image ID="Image11" runat="server" ImageUrl="~/img/Vert.png" Height="28px" />
                </td>
            </tr>
        </table>            
    </asp:Panel>
       
    <input type="hidden" id="coordinate_h" runat="server"/>
    <input type="hidden" id="coordinate_w" runat="server"/>
    <input type="hidden" id="coordinate_y" runat="server"/>
    <input type="hidden" id="coordinate_x" runat="server"/>

  <asp:Panel ID="Panel4" runat="server" Height="68px">
    <div class="msbt">
        <br />
          <table>
           <tr>
             <td class="auto-style23">
               <asp:Label ID="Label4" runat="server" class="btn btn-default btn-file">
                    <span>Parcourir...</span>
                    <asp:FileUpload ID="FU1" runat="server" Class="file" data-icon="false" /> 
               </asp:Label>             
             </td>
             <td class="auto-style23">
                    <asp:Button ID="btnUpLoad" class=" btn btn-primary" runat="server" Text="Charger..." OnClick="btnUpLoad_Click" />  
             </td>
           </tr>
           <tr>
             <td class="auto-style13">
                <div class="form-group">
                    <asp:Label ID="lblMsg" runat="server" ForeColor="Red"/>
                </div>
                 <div class="form-group" style="margin-left:100%">
                    <asp:Label ID="nbLettreTxt" runat="server" Text="Nombre de Caractère" Style="margin-top:10px;"/>
                </div>
             </td>
           </tr>
          </table>
    </div>
  </asp:Panel> 
   
  <asp:Panel ID="Panel1" runat="server" Height="482px" Width="499px">
        <table class="auto-style4">
            <tr>
                <td class="auto-style19">
                    <div class="form-group">
                        <asp:Label ID="Label10" runat="server" Text="Epaisseur(en mm)"></asp:Label>
                    </div>
                </td>
                <td class="auto-style19">
                    <asp:TextBox class="form-control" ID="TextBox4" runat="server" Width="280px" Height="40px"></asp:TextBox>
                </td>
            </tr>
       
            <tr>
                <td class="auto-style19">
                    <div class="form-group">
                        <asp:Label ID="Label3" runat="server" Text="Type Led"></asp:Label>
                    </div>
                </td>
                <td class="auto-style19">
				    
                    <asp:ScriptManager runat="server">
                    </asp:ScriptManager>

                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
						    <asp:TextBox class="form-control" ID="TxtLed" runat="server" Width="280px" Height="40px" ReadOnly ="true"></asp:TextBox>                   
					    </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
	        <tr>
                <td class="auto-style19">
                    <div class="form-group">
                        <asp:Label ID="LabelSearch" runat="server" Text="Rechercher"></asp:Label>
                    </div>
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:TextBox runat="server" ID="TxtSearch" class="form-control" Width="280px" Heigth="40px" placeholder="rechercher" OnTextChanged="TxtSearch_TextChanged" AutoPostBack="True" ViewStateMode="Enabled"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr> 
            <tr>
                <td/>
                <td class="auto-style19" style="padding-left:13px" >
                <%--Ad-hoc --%>
                    <div style="overflow:scroll ;height:150px ; width: 400px ;margin-left:-24%; border: 2px solid ; border-color:lightgrey ; border-radius:3px">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
							    <asp:GridView BorderStyle="None" AlternatingRowStyle-BackColor="#F0F0F0" ID="LEDList" runat="server" OnRowDataBound="List_DataBound" OnSelectedIndexChanged="List_SelectedIndexChanged" Width="100%" ShowHeaderWhenEmpty="True">
                                <%--<ce:ScrollableGridView  AlternatingRowStyle-BackColor="#F0F0F0" ID="LEDList" runat="server" OnRowDataBound="List_DataBound" OnSelectedIndexChanged="List_SelectedIndexChanged" Width="100%" Height="150px">--%>
								    <FooterStyle BackColor="White" ForeColor="#330099" />
								    <RowStyle BackColor="White" ForeColor="#330099" />
								    <SelectedRowStyle BackColor="#FFCC66" ForeColor="#663399" />
								    <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
								    <HeaderStyle BackColor="#F06300" Font-Bold="True" ForeColor="#FFFFCC" CssClass="FixedHead"/>
						       </asp:GridView>
						    </ContentTemplate>
                        </asp:UpdatePanel>
                       <style type="text/css">
                           .FixedHead {
                               position:relative;
                               text-align:center;
                               font-size:medium ;
                           }
                       </style>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="auto-style19">
                    <div class="form-group">
                        <asp:Label ID="Label6" runat="server" Text="Puissance en Watt"></asp:Label>
                    </div>
                </td>
                <td class="auto-style19">
			        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
						    <asp:TextBox ID="txtpuis" runat="server" class="form-control" Height="40px" Width="280px"></asp:TextBox>
					    </ContentTemplate>
                    </asp:UpdatePanel>
                </td> 
            </tr>
            <tr>
                <td class="auto-style19">
                    <div class="form-group">
                        <asp:Label ID="Label5" runat="server" Text="Longueur(en mm)"></asp:Label>
                    </div>
                </td>
                <td class="auto-style19">
                    <asp:TextBox class="form-control" ID="test" runat="server"  Width="280px" Height="40px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style22">
                    <div class="form-group">
                        <asp:Label ID="Label11" runat="server" Text="Hauteur(en mm)"></asp:Label>
                    </div>
                </td>
                <td class="auto-style22">
                    <asp:TextBox class="form-control" ID="haute" runat="server" Width="280px" Height="40px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style19">
                    <div class="form-group">
                        <asp:Label ID="Label7" runat="server" Text="Espacement(en mm)"></asp:Label>
                    </div>
                </td>
                <td class="auto-style19">
				    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
						    <asp:TextBox class="form-control"  ID="txtespace" runat="server" Width="280px" Height="40px"></asp:TextBox>
					    </ContentTemplate>
                    </asp:UpdatePanel>
                </td>   
            </tr>
            <tr>
                <td class="auto-style19">
                    <div class="form-group">
                        <asp:Label ID="Label8" runat="server" Text="Trait"></asp:Label>
                    </div>
                </td>
                <td class="auto-style19">
                    <asp:DropDownList  class="form-control" ID="DropDownList2" runat="server" Width="280px" Height="40px">
                        <asp:ListItem>Simple</asp:ListItem>
                        <asp:ListItem>Double</asp:ListItem>
                        <asp:ListItem>Triple</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="auto-style19"> 
                </td>
                <td class="auto-style19">
                    <asp:Button ID="Button1" runat="server" class=" btn btn-primary" OnClick="Button1_Click" Text="Calcul" Width="280px" Height="40px" />
                </td>
            </tr>
        </table>
  </asp:Panel>

  <asp:Panel ID="Panel2" runat="server" class="crop">
              <img src="" id="cropimg" runat="server" visible="false" style="margin-top: -40%;margin-left: 80%;"/>
  </asp:Panel> 
  <div id="rectangle">        
        <a id="minusBtn" onclick="minus()" style="cursor: pointer;"><img src="../Images/zoommoins.png" style="height: 5%;margin-left: 5%;" /></a>&nbsp;&nbsp;
        <a id="plusBtn" onclick="plus()" style="cursor: pointer;"><img src="../Images/zoomplus.png" style="height: 5%;" /></a>
  </div>
  <asp:Panel ID="panCrop" runat="server" visible="false">
        <table  class="image">
            <tr>
                <td>      
                    <asp:Image ID="imgUpload" runat="server" style="margin-left: 194px;"/>
                </td>
            </tr>
            <tr>
                <td> 
                    <asp:Button class=" btn btn-primary" ID="btnCrop" runat="server" OnClick="btnCrop_Click1" Text="Redimensionner"  visible="false" style="height:40px;width:280px;    margin-left: 16%;margin-top: 5%;"/>     
                </td>
                <td >
                    <asp:HiddenField ID="X" runat="server" />
                    <asp:HiddenField ID="Y" runat="server" />
                    <asp:HiddenField ID="W" runat="server" />
                    <asp:HiddenField ID="H" runat="server" />
                </td>
            </tr>
        </table>
  </asp:Panel>
  <asp:Panel ID="Panel" runat="server" >
    <table class="resultat">
        <tr>
            <td class="auto-style22">
                <asp:Label ID="Label2" runat="server" Text="R&eacute;sultats" style="" Font-Overline="False" Font-Size="Large" Font-Underline="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style22">
                <asp:Label ID="led" runat="server" Text="Nombre de LED"></asp:Label>
            </td>
            <td class="auto-style25">
                <asp:TextBox class="form-control"  ID="nbLed" runat="server" Width="280px" Height="40px"></asp:TextBox>
            </td>
            <td class="auto-style18">
                &nbsp;Modules
            </td>
        </tr>
        <tr>
            <td class="auto-style22">
                <asp:Label ID="pw" runat="server" Text="Puissance Totale"></asp:Label>
            </td>
            <td class="auto-style25">
                <asp:TextBox class="form-control"  ID="nbP" runat="server" Width="280px" Height="40px"></asp:TextBox>
            </td>
            <td class="auto-style18">
                &nbsp;Watts
            </td>
        </tr>
        <tr>
            <td class="auto-style22">
                <asp:Label ID="alim" runat="server" Text="Alimentation"></asp:Label>
            </td>  
            <td class="auto-style25">
                <asp:TextBox class="form-control"  ID="nbV" runat="server" Width="280px" Height="40px"></asp:TextBox>
            </td>
            <td class="auto-style18">
                &nbsp;U
            </td>
        </tr>
    </table>
  </asp:Panel>
</form>
</body>
</html>
