//Этот файл выполняется при запуске приложения
//1. Платформа для запуска модуля
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
//2. импорт главного модуля приложения.
import { AppModule } from './app/app.module';
//3. запуск главного модуля.
const platform = platformBrowserDynamic();
platform.bootstrapModule(AppModule);
