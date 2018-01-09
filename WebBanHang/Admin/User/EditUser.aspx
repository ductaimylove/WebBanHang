﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="EditUser.aspx.cs" Inherits="WebBanHang.Admin.User.EditUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <form runat="server">
        <div class="container-fluid">
        
        <div class="button-group">
            <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="Lưu" OnClick="btnSave_Click"/>
            <asp:Button ID="btnSaveNClose" CssClass="btn btn-primary" runat="server" Text="Lưu và đóng" OnClick="btnSaveNClose_Click" />
            <asp:Button ID="btnSaveNNew" CssClass="btn btn-primary" runat="server" Text="Lưu và tạo mới" OnClick="btnSaveNNew_Click"/>
            <a href="#" class="btn btn-primary">Đóng</a>
        </div>
        <div class="container">
            <ul id="error_messages" runat="server">
            </ul>
        </div>
        <div class="container">
            <ul id="success_messages" runat="server">

            </ul>
        </div>
        </div>
        <div class="container-fluid" >
        <div class="form-group">
            <label for="username">Tên tài khoản</label>
            <input id="username" class="form-control" disabled="disabled" runat="server"></input>
        </div>
            <div class="form-row">
        <div class="col-md-6">
            <label for="lastname">Họ</label>
            <input type="text" class="form-control" id="lastname" runat="server" required="required"/>
        </div>
        <div class="col-md-6">
            <label for="firstname">Tên</label>
            <input type="text" class="form-control" id="firstname" runat="server" required="required"/>
        </div>
                </div>
        <div class="form-group">
            <label for="email">Email</label>
            <input type="text" class="form-control" id="email" runat="server" required="required"/>
        </div>
            </div>
        <div class="card">
            <div class="card-header">
                <a data-toggle="collapse" href="#collapse1" aria-expanded="false" aria-controls="collapse1">Quyền hạn</a>
            </div>
            <div class="card-block">
                <div id="collapse1" class="panel-collapse collapse">
                    <asp:checkboxlist runat="server" id="cblPermissions"></asp:checkboxlist>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
