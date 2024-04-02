import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { PessoaService } from '../service/pessoa.service';

import { CommonModule } from '@angular/common';
import {MatListModule} from '@angular/material/list';
import { MatCardModule } from '@angular/material/card';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatIconModule } from '@angular/material/icon';
import { ListComponent } from '../list/list.component';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { HomeComponent } from '../home/home.component';
import { Pessoa } from '../inferface/Pessoa';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet,ListComponent,
HomeComponent],
providers:[PessoaService],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {
  title = 'GeradorPdfWeb';
listaDePessoas:Pessoa[]=[];

constructor(private pessoaservice:PessoaService,private snackBar:MatSnackBar
,private router: Router,
    private cdr: ChangeDetectorRef,){}
ngOnInit() {
    this.obterTodasPessoas();
  }

 obterTodasPessoas() {
    this.pessoaservice.obterTodasPessoa().subscribe(
      pessoas => {
        this.listaDePessoas = pessoas;
      },
      error => {
        console.error('Erro ao obter pessoas:', error);
      }
    );
  }
  baixarPdfPessoa(id: string) {
  const idNumber = parseInt(id, 10); // Converte a string para um número
  this.pessoaservice.baixarPdfPessoa(idNumber).subscribe(pdfData => {
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
 navegarParaListagem() {
    this.router.navigateByUrl('/listagem').then(() => {
      this.cdr.detectChanges(); // Forçar a detecção de mudanças após a navegação
    });


  }


}