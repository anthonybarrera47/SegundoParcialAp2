<%@ Page
    Title="Consulta de transacciones"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="ConsultaTransacciones.aspx.cs"
    Inherits="SegundoParcialAp2.UI.Consultas.ConsultaTransacciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>




<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <script type="text/javascript">
        function ShowReporte(title) {
            $("#ModalReporte .modal-title").html(title);
            $("#ModalReporte").modal("show");
        }
    </script>
        <style type="text/css">
        .bigModal {
            width: 1080px;
            height:800px;
            margin-left: -200px;
        }
    </style>
    <%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"
        Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

    <div class="container-fluid">
        <div class="card text-center bg-light mb-3">
            <div class="card-header"><%:Page.Title %></div>
            <div class="card-body">
                <div class="form-horizontal col-md-12" role="form">
                    <div>
                        <div class="input-group mb-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text" id="FiltroDropDownList">Filtro </span>
                            </div>
                            <div class="input-group-prepend col-md-4" aria-describedby="FiltroDropDownList">
                                <asp:DropDownList ID="BuscarPorDropDownList" runat="server" CssClass="form-control input-sm">
                                    <asp:ListItem>Todos</asp:ListItem>
                                    <asp:ListItem>Transaccion ID</asp:ListItem>
                                    <asp:ListItem>Cliente ID</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="input-group-prepend">
                                <span class="input-group-text" id="CriterioLB">Criterio </span>
                            </div>
                            <div class="input-group-append" aria-describedby="CriterioLB">
                                <asp:TextBox ID="FiltroTextBox" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </div>
                            <div class="input-group-append" aria-describedby="FiltroTextBox">
                                <asp:Button ID="BuscarButton" runat="server" Class="btn btn-success input-sm" Text="Buscar" OnClick="BuscarButton_Click" />
                            </div>
                        </div>
                    </div>
                    <%--CheckBox--%>
                    <div class="form-row">
                        <div class="custom-control custom-checkbox mr-sm-2">
                            <asp:CheckBox CssClass="custom-checkbox" Checked="true" ID="FechaCheckBox" runat="server" Text="Filtrar por fecha" />
                        </div>
                    </div>
                    <%--FechaDesde--%>
                    <div class="input-group mb-3">
                        <div class="input-group-prepend">
                            <span class="input-group-text" id="FechaDesde">Desde </span>
                        </div>
                        <div class="input-group-append" aria-describedby="FechaDesdeTextBox">
                            <asp:TextBox ID="FechaDesdeTextBox" TextMode="Date" runat="server" class="form-control input-sm" Visible="true"></asp:TextBox>
                        </div>
                        <div class="input-group-prepend">
                            <span class="input-group-text" id="FechaHasta">Hasta </span>
                        </div>
                        <div class="input-group-append" aria-describedby="FechaHastaTextBox">
                            <asp:TextBox ID="FechaHastaTextBox" TextMode="Date" runat="server" class="form-control input-sm" Visible="true"></asp:TextBox>
                        </div>
                    </div>
                    <%--GRID--%>

                    <asp:UpdatePanel ID="UpdatePanel" runat="server">
                        <ContentTemplate>
                            <div class="table table-condensed table-bordered table-responsive">
                                <asp:GridView ID="DatosGridView"
                                    runat="server"
                                    CssClass="table table-condensed table-bordered table-responsive"
                                    CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="false">

                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <Columns>
                                        <asp:TemplateField ShowHeader="False" HeaderText="Opciones">
                                            <ItemTemplate>
                                                <asp:Button ID="ImprimirButton" runat="server" CausesValidation="false" CommandName="Select"
                                                    Text="Imprimir" CssClass="btn btn-info" OnClick="ImprimirButton_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Fecha" DataField="Fecha" DataFormatString="{0:dd-MM-yyyy}" />
                                        <asp:BoundField HeaderText="Transaccion" DataField="TransaccionID" />
                                        <asp:BoundField HeaderText="Cliente" DataField="ClienteID" Visible="false" />
                                        <asp:BoundField HeaderText="Cliente" DataField="NombreCliente" />
                                    </Columns>
                                </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="DatosGridView" />
                        </Triggers>
                    </asp:UpdatePanel>

                </div>
            </div>
        </div>
    </div>
    <%-- El  Modal para el Reporte--%>
    <div class="modal fade" id="ModalReporte" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content bigModal">
                <div class="modal-header">
                    <h5 class="modal-title" id="ModalLebel">Reporte de Transacciones</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <%--Viewer--%>
                    <rsweb:ReportViewer ID="ReportViewer" runat="server" ProcessingMode="Remote" Height="100%" Width="100%">
                    </rsweb:ReportViewer>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
