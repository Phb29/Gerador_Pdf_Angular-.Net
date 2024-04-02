import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { ListComponent } from '../list/list.component';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatButton } from '@angular/material/button';

import { PessoaService } from '../service/pessoa.service';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { FormsModule } from '@angular/forms';
import { NgForm } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { Pessoa } from '../inferface/Pessoa';
import { MatSnackBar } from '@angular/material/snack-bar';
@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    RouterOutlet,
    RouterLink,
    FlexLayoutModule,
    FormsModule,
    MatButton,
    CommonModule,
    MatFormFieldModule,
    MatInputModule,
  ],
  providers: [PessoaService],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
})
export class HomeComponent {
  pessoa: Pessoa = {
    nome: '',
    email: '',
    profissao: '',
    habilidade: '',
  };
  constructor(
    private pessoaService: PessoaService,
    private snack: MatSnackBar
  ) {}
  adicionarPessoa(form: NgForm): void {
    this.pessoaService.criarPessoa(this.pessoa).subscribe(
      (novaPessoa: Pessoa) => {
        form.reset();
      this.mostrarSnackBar('Nova Pessoa adicionada com Sucesso!');
      },
      (error) => {
        console.error('Erro ao adicionar pessoa:', error);
      }
    );
  }
  mostrarSnackBar(mensagem: string) {
    this.snack.open(mensagem, 'Fechar', {
      duration: 3000, // Duração em milissegundos
      horizontalPosition: 'center', // Posição horizontal
      verticalPosition: 'top', // Posição vertical
    });
  }
}
