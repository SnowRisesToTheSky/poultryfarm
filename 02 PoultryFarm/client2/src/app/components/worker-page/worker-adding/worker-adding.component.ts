import {Component, Input, OnInit} from '@angular/core';
import {Worker1} from "../../../models";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {Router} from "@angular/router";

@Component({
  selector: 'app-worker-adding',
  templateUrl: './worker-adding.component.html',
  styleUrls: ['./worker-adding.component.css']
})
export class WorkerAddingComponent implements OnInit {

  //ПОЛЯ
  //1. Была ли попытка отправки формы
  public wasSubmitTrying:boolean=false

  //ДЛЯ РЕАКТИВНЫХ ФОРМ
  //1. Мин/макс. кол-во символов в разных полях
  public nameMinLenght=2
  public nameMaxLenght=40
  public passportMinLenght=8
  public passportMaxLenght=16
  public salaryMinLenght=4

  //2. Рег. выражения для проверки полей ввода
  public namePattern="[А-Яа-яA-Za-zА-Яа-я -]*";
  public passportPattern="[А-Яа-яA-Za-zА-Яа-я0-9]*";
  public salaryPattern="[0-9]*";

  //3. Будем использовать реактивную форму
  public addingForm:FormGroup
  //4. Объект с сообщениями ошибках
  public validationMessages = {
    "surname": {
      "ok":"Всё правильно!",
      "required": "Обязательное поле.",
      "minlength": `Символов д.б. от ${this.nameMinLenght} и более.`,
      "maxlength": `Значение не должно быть больше ${this.nameMaxLenght} символов.`,
      "pattern":"Тут можно вводить только русские, украинские или английские алфавитные символы! А также дефис и пробел."
    },
    "name": {
      "ok":"Всё правильно!",
      "required": "Обязательное поле.",
      "minlength": `Символов д.б. от ${this.nameMinLenght} и более.`,
      "maxlength": `Значение не должно быть больше ${this.nameMaxLenght} символов.`,
      "pattern":"Тут можно вводить только русские, украинские или английские алфавитные символы! А также дефис и пробел."
    },
    "patronymic": {
      "ok":"Всё правильно!",
      "required": "Обязательное поле.",
      "minlength": `Символов д.б. от ${this.nameMinLenght} и более.`,
      "maxlength": `Значение не должно быть больше ${this.nameMaxLenght} символов.`,
      "pattern":"Тут можно вводить только русские, украинские или английские алфавитные символы! А также дефис и пробел."
    },
    "pasport": {
      "ok":"Всё правильно!",
      "required": "Обязательное поле.",
      "minlength": `Символов д.б. от ${this.passportMinLenght} и более.`,
      "maxlength": `Значение не должно быть больше ${this.passportMaxLenght} символов.`,
      "pattern":"Тут можно вводить только русские, украинские или английские алфавитные символы! А также цифры."
    },
    "salary": {
      "ok":"Всё правильно!",
      "required": "Обязательное поле.",
      "minlength": `Кол-во цифр д.б. равно или больше ${this.salaryMinLenght}`,
      "pattern": "Тут можно вводить только целые числа."
    },
  };


  //КОНСТРУКТОР
  constructor(private router: Router) { }

  //МЕТОДЫ ВРЕМЕНИ ЖИЗНИ КОМПОНЕНТА
  ngOnInit(): void {
    //Сущности для полей реактивной формы
    this.addingForm = new FormGroup({
      workerSurname: new FormControl("",[
        Validators.required,
        Validators.minLength(this.nameMinLenght),
        Validators.maxLength(this.nameMaxLenght),
        Validators.pattern(this.namePattern),
      ]),
      workerName: new FormControl("",[
        Validators.required,
        Validators.minLength(this.nameMinLenght),
        Validators.maxLength(this.nameMaxLenght),
        Validators.pattern(this.namePattern),
      ]),
      workerPatronymic: new FormControl("",[
        Validators.required,
        Validators.minLength(this.nameMinLenght),
        Validators.maxLength(this.nameMaxLenght),
        Validators.pattern(this.namePattern),
      ]),
      workerPassport: new FormControl("",[
        Validators.required,
        Validators.minLength(this.passportMinLenght),
        Validators.maxLength(this.passportMaxLenght),
        Validators.pattern(this.passportPattern),
      ]),
      workerSalary: new FormControl("",[
        Validators.required,
        Validators.minLength(this.salaryMinLenght),
        Validators.pattern(this.salaryPattern),
      ]),
    });
  }

  //ДРУГИЕ МЕТОДЫ
  //Добавить рабочего
  onSubmit(form:FormGroup) {
    //1. Помечаем что уже была попытка добавления
    this.wasSubmitTrying = true;
    //2. Если при этом обнаружены ошибки - добавление отменяется
    for(let ctrl in form.controls){
      if(!!form.controls[ctrl].errors)return;
    }
    //3. Если ошибок не было - спокойно добавляем и переходим к списку
    this.router.navigate(['/workers/look-at-records','record-added']);
  }

  //Очистить поля
  clearFields(){
    this.addingForm.get("workerSurname").patchValue("");
    this.addingForm.get("workerName").patchValue("");
    this.addingForm.get("workerPatronymic").patchValue("");
    this.addingForm.get("workerPassport").patchValue("");
    this.addingForm.get("workerSalary").patchValue("");
  }

  //Просто вернуться на пред. страницу
  toRecordList(){
    this.router.navigate(['/workers/look-at-records','just-show-records']);
  }

}
