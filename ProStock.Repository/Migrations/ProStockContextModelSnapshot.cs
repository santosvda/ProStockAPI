﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProStock.Repository;

namespace ProStock.Repository.Migrations
{
    [DbContext(typeof(ProStockContext))]
    partial class ProStockContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProStock.Domain.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Ativo");

                    b.Property<int>("PessoaId");

                    b.HasKey("Id");

                    b.HasIndex("PessoaId");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("ProStock.Domain.Endereco", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Ativo");

                    b.Property<string>("Bairro");

                    b.Property<string>("Cep");

                    b.Property<string>("Cidade");

                    b.Property<string>("Complemento");

                    b.Property<DateTime>("DataExclusao");

                    b.Property<DateTime>("DataInclusao");

                    b.Property<string>("Pais");

                    b.Property<int>("PessoaId");

                    b.Property<string>("Rua");

                    b.Property<string>("Uf");

                    b.HasKey("Id");

                    b.HasIndex("PessoaId");

                    b.ToTable("Enderecos");
                });

            modelBuilder.Entity("ProStock.Domain.Estoque", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Ativo");

                    b.Property<DateTime>("DataAlteracao");

                    b.Property<int>("ProdutoId");

                    b.Property<int>("QtdAtual");

                    b.Property<int>("QtdMinima");

                    b.Property<int>("QtdReservada");

                    b.HasKey("Id");

                    b.HasIndex("ProdutoId");

                    b.ToTable("Estoques");
                });

            modelBuilder.Entity("ProStock.Domain.Loja", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Ativo");

                    b.Property<DateTime>("DataExclusao");

                    b.Property<DateTime>("DataInclusao");

                    b.Property<string>("Descricao");

                    b.Property<string>("Email");

                    b.Property<int>("EnderecoId");

                    b.Property<string>("Telefone");

                    b.HasKey("Id");

                    b.HasIndex("EnderecoId");

                    b.ToTable("Lojas");
                });

            modelBuilder.Entity("ProStock.Domain.Pessoa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Ativo");

                    b.Property<string>("Cpf");

                    b.Property<DateTime>("DataExclusao");

                    b.Property<DateTime>("DataInclusao");

                    b.Property<string>("Email");

                    b.Property<string>("Nome");

                    b.Property<string>("Telefone");

                    b.HasKey("Id");

                    b.ToTable("Pessoas");
                });

            modelBuilder.Entity("ProStock.Domain.Produto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Ativo");

                    b.Property<DateTime>("DataExclusao");

                    b.Property<DateTime>("DataInclusao");

                    b.Property<string>("Descricao");

                    b.Property<string>("Marca");

                    b.Property<string>("Nome");

                    b.Property<int>("UsuarioId");

                    b.Property<decimal>("ValorUnit");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Produtos");
                });

            modelBuilder.Entity("ProStock.Domain.ProdutoVenda", b =>
                {
                    b.Property<int>("ProdutoId");

                    b.Property<int>("VendaId");

                    b.HasKey("ProdutoId", "VendaId");

                    b.HasIndex("VendaId");

                    b.ToTable("ProdutosVendas");
                });

            modelBuilder.Entity("ProStock.Domain.TipoUsuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Ativo");

                    b.Property<string>("Descricao");

                    b.HasKey("Id");

                    b.ToTable("TiposUsuarios");
                });

            modelBuilder.Entity("ProStock.Domain.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Ativo");

                    b.Property<DateTime>("DataExclusao");

                    b.Property<DateTime>("DataInclusao");

                    b.Property<string>("Login");

                    b.Property<int>("LojaId");

                    b.Property<int>("PessoaId");

                    b.Property<string>("Senha");

                    b.Property<int>("TipoUsuarioId");

                    b.HasKey("Id");

                    b.HasIndex("LojaId");

                    b.HasIndex("PessoaId");

                    b.HasIndex("TipoUsuarioId");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("ProStock.Domain.Venda", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Ativo");

                    b.Property<int>("ClienteId");

                    b.Property<DateTime>("Data");

                    b.Property<DateTime>("DataExclusao");

                    b.Property<int>("UsuarioId");

                    b.Property<decimal>("ValorTotal");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Vendas");
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
                        .HasForeignKey("PessoaId")
                        .OnDelete(DeleteBehavior.Cascade);
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
                        .HasForeignKey("EnderecoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProStock.Domain.Produto", b =>
                {
                    b.HasOne("ProStock.Domain.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade);
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
                        .HasForeignKey("LojaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProStock.Domain.Pessoa", "Pessoa")
                        .WithMany()
                        .HasForeignKey("PessoaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProStock.Domain.TipoUsuario", "TipoUsuario")
                        .WithMany()
                        .HasForeignKey("TipoUsuarioId")
                        .OnDelete(DeleteBehavior.Cascade);
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
