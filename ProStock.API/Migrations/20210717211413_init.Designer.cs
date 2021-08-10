﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProStock.Repository;

namespace ProStock.API.Migrations
{
    [DbContext(typeof(ProStockContext))]
    [Migration("20210717211413_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ProStock.Domain.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Ativo")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<DateTime?>("DataExclusao");

                    b.Property<DateTime>("DataInclusao");

                    b.Property<int>("PessoaId");

                    b.HasKey("Id");

                    b.HasIndex("Ativo");

                    b.HasIndex("PessoaId");

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("ProStock.Domain.Endereco", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Ativo")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("Bairro")
                        .HasMaxLength(50);

                    b.Property<string>("Cep")
                        .HasMaxLength(9);

                    b.Property<string>("Cidade")
                        .HasMaxLength(50);

                    b.Property<string>("Complemento")
                        .HasMaxLength(70);

                    b.Property<DateTime?>("DataExclusao");

                    b.Property<DateTime>("DataInclusao");

                    b.Property<int>("Numero")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0);

                    b.Property<string>("Pais")
                        .HasMaxLength(50);

                    b.Property<int?>("PessoaId");

                    b.Property<string>("Rua")
                        .HasMaxLength(50);

                    b.Property<string>("Uf")
                        .HasMaxLength(2);

                    b.HasKey("Id");

                    b.HasIndex("Ativo");

                    b.HasIndex("PessoaId");

                    b.ToTable("Endereco");
                });

            modelBuilder.Entity("ProStock.Domain.Estoque", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Ativo")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<DateTime>("DataAlteracao");

                    b.Property<DateTime?>("DataExclusao");

                    b.Property<DateTime>("DataInclusao");

                    b.Property<int>("ProdutoId");

                    b.Property<int>("QtdAtual")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0);

                    b.Property<int>("QtdMinima")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0);

                    b.Property<int>("QtdReservada")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0);

                    b.HasKey("Id");

                    b.HasIndex("Ativo");

                    b.HasIndex("ProdutoId");

                    b.ToTable("Estoque");
                });

            modelBuilder.Entity("ProStock.Domain.Loja", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Ativo")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<DateTime?>("DataExclusao");

                    b.Property<DateTime>("DataInclusao");

                    b.Property<string>("Descricao")
                        .HasMaxLength(70);

                    b.Property<string>("Email")
                        .HasMaxLength(50);

                    b.Property<int?>("EnderecoId");

                    b.Property<string>("Telefone")
                        .HasMaxLength(14);

                    b.HasKey("Id");

                    b.HasIndex("Ativo");

                    b.HasIndex("EnderecoId");

                    b.ToTable("Loja");
                });

            modelBuilder.Entity("ProStock.Domain.Pessoa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Ativo")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("Cpf")
                        .HasMaxLength(11);

                    b.Property<DateTime?>("DataExclusao");

                    b.Property<DateTime>("DataInclusao");

                    b.Property<string>("Email")
                        .HasMaxLength(50);

                    b.Property<string>("Nome")
                        .HasMaxLength(70);

                    b.Property<string>("Telefone")
                        .HasMaxLength(14);

                    b.HasKey("Id");

                    b.HasIndex("Ativo");

                    b.ToTable("Pessoa");
                });

            modelBuilder.Entity("ProStock.Domain.Produto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Ativo")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<DateTime?>("DataExclusao");

                    b.Property<DateTime>("DataInclusao");

                    b.Property<string>("Descricao")
                        .HasMaxLength(200);

                    b.Property<string>("Marca")
                        .HasMaxLength(50);

                    b.Property<string>("Nome")
                        .HasMaxLength(70);

                    b.Property<int?>("UsuarioId");

                    b.Property<decimal>("ValorUnit")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(10, 2)")
                        .HasDefaultValue(0m);

                    b.HasKey("Id");

                    b.HasIndex("Ativo");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Produto");
                });

            modelBuilder.Entity("ProStock.Domain.ProdutoVenda", b =>
                {
                    b.Property<int>("ProdutoId");

                    b.Property<int>("VendaId");

                    b.Property<int>("Quantidade");

                    b.HasKey("ProdutoId", "VendaId");

                    b.HasIndex("VendaId");

                    b.ToTable("ProdutosVendas");
                });

            modelBuilder.Entity("ProStock.Domain.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Ativo")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<DateTime?>("DataExclusao");

                    b.Property<DateTime>("DataInclusao");

                    b.Property<string>("Login")
                        .HasMaxLength(20);

                    b.Property<int?>("LojaId");

                    b.Property<int?>("PessoaId");

                    b.Property<string>("Senha")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.HasIndex("Ativo");

                    b.HasIndex("LojaId");

                    b.HasIndex("PessoaId");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("ProStock.Domain.Venda", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Acrescimo")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<bool>("Ativo")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<int>("ClienteId");

                    b.Property<DateTime>("Data");

                    b.Property<DateTime?>("DataExclusao");

                    b.Property<DateTime>("DataInclusao");

                    b.Property<decimal>("Desconto")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<string>("Descricao")
                        .HasMaxLength(50);

                    b.Property<decimal>("Frete")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<string>("Status")
                        .HasMaxLength(50);

                    b.Property<int>("UsuarioId");

                    b.Property<decimal>("ValorTotal")
                        .HasColumnType("decimal(10, 2)");

                    b.HasKey("Id");

                    b.HasIndex("Ativo");

                    b.HasIndex("ClienteId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Venda");
                });

            modelBuilder.Entity("ProStock.Domain.Cliente", b =>
                {
                    b.HasOne("ProStock.Domain.Pessoa", "Pessoa")
                        .WithMany()
                        .HasForeignKey("PessoaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProStock.Domain.Endereco", b =>
                {
                    b.HasOne("ProStock.Domain.Pessoa", "Pessoa")
                        .WithMany("Enderecos")
                        .HasForeignKey("PessoaId");
                });

            modelBuilder.Entity("ProStock.Domain.Estoque", b =>
                {
                    b.HasOne("ProStock.Domain.Produto", "Produto")
                        .WithMany()
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProStock.Domain.Loja", b =>
                {
                    b.HasOne("ProStock.Domain.Endereco", "Endereco")
                        .WithMany()
                        .HasForeignKey("EnderecoId");
                });

            modelBuilder.Entity("ProStock.Domain.Produto", b =>
                {
                    b.HasOne("ProStock.Domain.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId");
                });

            modelBuilder.Entity("ProStock.Domain.ProdutoVenda", b =>
                {
                    b.HasOne("ProStock.Domain.Produto", "Produto")
                        .WithMany("ProdutosVendas")
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProStock.Domain.Venda", "Venda")
                        .WithMany("ProdutosVendas")
                        .HasForeignKey("VendaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProStock.Domain.Usuario", b =>
                {
                    b.HasOne("ProStock.Domain.Loja", "Loja")
                        .WithMany("Usuarios")
                        .HasForeignKey("LojaId");

                    b.HasOne("ProStock.Domain.Pessoa", "Pessoa")
                        .WithMany()
                        .HasForeignKey("PessoaId");
                });

            modelBuilder.Entity("ProStock.Domain.Venda", b =>
                {
                    b.HasOne("ProStock.Domain.Cliente", "Cliente")
                        .WithMany("Vendas")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProStock.Domain.Usuario", "Usuario")
                        .WithMany("Vendas")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}