import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterModule, RouterOutlet } from '@angular/router';
import { FlexLayoutModule } from '@angular/flex-layout';
import { PessoaService } from '../service/pessoa.service';

import { error } from 'console';
import { MatListModule } from '@angular/material/list';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatButton, MatButtonModule } from '@angular/material/button';
import { Pessoa } from '../inferface/Pessoa';

@Component({
  selector: 'app-list',
  standalone: true,
  imports: [RouterOutlet,CommonModule,MatListModule,MatCardModule,MatSlideToggleModule
,MatSnackBarModule,MatIconModule,RouterLink, FlexLayoutModule,MatButton,MatButtonModule],
  providers: [PessoaService],
  templateUrl: './list.component.html',
  styleUrl: './list.component.scss',
})
export class ListComponent implements OnInit {
  listaDePessoas: Pessoa[] = [];
  pessoaEditando: Pessoa | null = null;
  ngOnInit() {
    this.obterTodasPessoas();
  }
  constructor(private pessoaService: PessoaService,private snackBar: MatSnackBar) {}

  obterTodasPessoas() {
    return this.pessoaService.obterTodasPessoa().subscribe(
      (pessoas) => {
        this.listaDePessoas = pessoas;
      },
      (error) => {
        console.log('errro obter pessoa', error);
      }
    );
  }
baixarPdfPessoa(id: string) {
  const idNumber = parseInt(id, 10); // Converte a string para um número
  this.pessoaService.baixarPdfPessoa(idNumber).subscribe(pdfData => {
    const blob = new Blob([pdfData], { type: 'application/pdf' });
    const url = window.URL.createObjectURL(blob);

    // Criar um link temporário
    const link = document.createElement('a');
    link.href = url;
    link.download = 'informacoes_pessoa.pdf'; // Nome do arquivo para download

    // Adicionar o link ao documento
    document.body.appendChild(link);

    // Iniciar o download
    link.click();

    // Remover o link após o download
    document.body.removeChild(link);

    // Limpar o URL do blob após o download
    window.URL.revokeObjectURL(url);
  });
}
 editarPessoa(pessoa: Pessoa) {
    this.pessoaEditando = pessoa;
  }

  cancelarEdicao() {
    this.pessoaEditando = null;
  }

 
baixarPdf() {
    this.pessoaService.baixarPdf().subscribe(
      (pdfData: Blob) => {
        // Aqui você pode manipular o Blob retornado, por exemplo, abrindo-o em uma nova aba do navegador
        const url = window.URL.createObjectURL(pdfData);
        window.open(url);
      },
      (error) => {
        console.error('Erro ao baixar PDF:', error);
      }
    );
  }

}