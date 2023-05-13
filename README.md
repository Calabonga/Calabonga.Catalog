# Каталог товаров и услуг 2023

Платформа: ASP.NET Core NET 7.0

## Краткое описание
Каталог создается заново. Некоторые сущности переиспользуются. Ну, и конечно же будут переиспользованы некоторые механизмы, а некоторые будут написаны заново.
## Из чего состоит проект

В реализации будут использоваться следующие технологии, сборки, фреймворки, подходы, паттерны (т.д. и т.п.). Другими словами, ключевые понятия, которые можно встретить в проекте:

* База данных PostgreSQL
* EntityFrameworkCore
* Unit of Work
* Mediatr
* FluentValidation
* PredicatesBuilder
* OperationResult (as RFC7807)
* Microsoft Identity
* Vertical Slice Architecture
* Minimal API
* OpenIddict Auth2.0
* Nimble Framework (Microservice Template)
* Swagger
* AppDefinitions
# Требования, правила, бизнес-логика
## Роли в системе

В проекте используется две роли, которые регламентируют доступ к функционалу.
`Administrator` - (главная роль) пользователь, у которого есть эта роль, может выполнять все операции с любыми сущностями).
`User` - пользоватеть, у которого есть эта роль может добавлять обзоры к товарам.

Незарегистрированные пользователи работают с системой в режиме "readonly".
## Сущность "Category"
* [x] `Name` должно быть не менее 5 и не более 50 символов.
* [x] `Description` должно быть не более 1024 символов, но может быть пустым.
* [x] `Category` можно создать без товаров.
* [ ] `Category` можно выключить/выключить (скрыть/показать для всеобщего просмотра).
* [ ] При выключении каталога все товары в каталоге тоже должны выключиться.
* [ ] При включении каталога необходимо явно указать, включать или не включать товары.
* [x] При получение всех товаров `GetAll()` администратор должен получать и скрытые категории тоже.
* [ ] Просмотр всех каталогов должно использоваться разбиение на страницы (paging)
* [x] При создании нового каталога, он должен быть невидимый по умолчанию.
* [x] API должна содержать методы CRUD для управления сущностью `Category`:
	* [x] `GetPaged(int pageIndex, int pageSize)`
	* [x] `GetAll()`
	* [x] `Create(CategoryViewModel model)`
	* [x] `GetById(Guid id)`
	* [x] `Update(CategoryUpdateViewModel)`
	* [x] `Delete(Guid id)`
---
## Сущность "Product"
* [x] `Name` должно быть не менее 5 и не более 128 символов.
* [x] `Description` должно быть не более 2048 символов, но может быть пустым.
* [x] `Price` может быть не задана (null).
* [x] `CategoryId` обязательно при создании нового товара.
* [x] `Product` можно выключить/выключить (скрыть/показать для всеобщего просмотра).
* [ ] Товар нельзя включить, если каталог товара выключен
* [ ] Просмотр всех товаров должно использоваться разбиение на страницы (paging)
* [x] При создании товар он должен быть невидимый.
* [ ] API должна содержать методы CRUD для управления сущностью `Product`:
	* [x] `GetPaged(int pageIndex, int pageSize)`
	* [x] `GetAll()` (глупый метод)
	* [x] `Create(ProductCreateViewModel model)` Get
	* [x] `Create(ProductCreateViewModel model)` Post
	* [x] `GetById(Guid id)`
	* [x] `Update(ProductUpdateViewModel)`Get
	* [x] `Update(ProductUpdateViewModel)`Put
	* [x] `Delete(Guid id)`
	* [x] `GetMostReviewed(int count = 10)`
	* [x] `GetMostRated(int count = 10)`
---
## Сущность "Review"
* [ ] `UserName` должно быть не менее 5 и не более 128 символов
* [ ] `Content` должно быть не более 2048 символов, но может быть пустым
* [ ] `Rating` должно быть от 1 до 5 единиц (очков, баллов, и т.д.)
* [ ] `ProductId` обязательно при создании нового обзора (комментария)
* [ ] Посмотреть все обзоры для товаров можно лишь только администратору.
* [ ] Просмотр всех обзоров должны использоваться разбиение на страницы (paging)
* [ ] `Review` можно тоже выключить и включить в соответствии со статусом товара, для которого это review написано.
* [ ] API должна содержать методы CRUD для управления сущностью `Review`:
	* [x] `Create(ReviewCreateViewModel model)` Get
	* [x] `Create(ReviewCreateViewModel model)` Post
	* [ ] `GetById(Guid id)`
	* [ ] `Update(ReviewUpdateViewModel)`
	* [ ] `Delete(Guid id)`
	* [ ] `GetLastReviews(int count)`
	* [ ] `GetPaged(int pageIndex, int pageSize)`
	* [ ] `GetAll(Guid productId)`
---
## Сущность "Tag" ("Метка")
* [x] Один продукт должен иметь одну и более меток (до 8 шт).
* [x] При создании и при редактировании товара к нему можно добавить несколько меток. Если метка не существует в системе, то она создается. Если метка уже существует, то к товару привязывается ссылка нее.
* [x] Если из описания товара удаляется метка, то надо проверить что эта метка не используется других товарах. Если метка больше нигде не используется, ее требуется удалить.
* [ ] Данные о продукте должны включать в себя метки товара (`GetAll()`, `GetById` и `GetPaged`)
* [ ] Просмотр всех меток, которые используются в каталоге можно осуществить на странице "Облако меток" (см. `GetCloud()`).
* [ ] API должна содержать методы CRUD для управления сущностью `Tag`:
	* [ ] `GetCloud()`
	* [ ] `Update(TagUpdateViewModel)`
	* [ ] `Delete(Guid id)`
---
## Общие требования для сущности Review

* [ ] Товар может иметь несколько отзывов или не иметь вообще.
* [ ] Отзыв может оставить только зарегистрированный пользователь ролью `User`
* [ ] Отзыв должен содержать следующие обязательные свойства: `Id`, `Content`, `Rating`, `UserName`
* [ ] Список последних 10 отзывов может быть также запрошен на UI (см. `GetLastReview(int count)`).
* [ ] При сокрытии товара от всеобщего просмотра, отзывы о товаре тоже не должны нигде отображаться.
---
## Требования для роли Администратор

* [ ] Администратор должен иметь возможность удалить любой отзыв
* [ ] Администратор должен иметь возможность удалять товары
* [ ] Администратор должен иметь возможность удалять пользователей
* [ ] Администратор должен иметь возможность удалять метки
* [ ] Администратор должен иметь возможность создавать категории
---
## Требования для роли Пользователь

* [ ] Пользователь может добавить сколько угодно отзывов на любой товар
* [ ] Пользователь может удалить свой и только свой отзыв
* [ ] Пользователь может изменить рейтинг своего отзыва и текст содержания
## Диаграмма классов

``` mermaid
classDiagram
	direction RL
	Product "*" <-- "1" Category
	Tag "1..8" <-- "*" Product
	Review "*" <-- "1" Product
	Auditable <|-- Identity
	Category <|-- Identity
	EventItem <|-- Identity
	Tag <|-- Identity
	Product <|-- Auditable
	Review <|-- Auditable

```

``` mermaid
---
title: Сущность Category
---
classDiagram
class Category {
	+string Name
	+string Description
	+List~Product~
	bool Visible
}
```

``` mermaid
---
title: Сущность Product
---
classDiagram
class Product {
	+string Name
	+string Description
	Guid CategoryId
	+int Price
	+List~Review~
	+List~Tag~
	bool Visible
}
```

``` mermaid
---
title: Сущность EventItem
---
classDiagram
class EventItem {
	DateTime CreatedAt
	string Logger
	string Level
	string Message
	string? ThreadId
	string? ExceptionMessage
}
```

``` mermaid
---
title: Сущность Tag
---
classDiagram
class Tag {
	string Name
	List<Product>? Products
}
```

``` mermaid
---
title: Сущность Review
---
classDiagram
class Review {
	string Content
	string User
	int Rating
	Guid Product
	virtual Product
	bool Visible
}
```

``` mermaid
---
title: Сущность Identity
---
classDiagram
class Identity{
	<<Abstract>>
	+Guid Id
}
```

``` mermaid
---
title: Сущность Auditable
---
classDiagram
class Auditable{
	<<Abstract>>
	DateTime CreatedAt
	string CreatedBy
	DateTime? UpdatedAt
	string? UpdatedBy
}
```
# Каталог товаров и услуг 2019

Платформа: ASP.NET Core 2.2

## Краткое описание

Проект создается самого начала. Используется шаблон, который описан в видео. В процессе разработки показывается как программировать, как создавать правильный функциональный код. Как использовать паттерны. Описываются принципы и правила.

[Исходные файлы версии 2019](https://github.com/Calabonga/Calabonga.Catalog/releases/tag/v2019)

## Видео 2019
Создание приложение подробно запечатлено на видео.

[Каталог товаров 2019](https://www.youtube.com/playlist?list=PLIB8be7sunXOiIeeUa6yItyHpLtKG9gqQ)

## Статья в блоге 2019
Описание проекта, а также возможность связаться с автором есть в блоге. Статья описывающая данный репозиторий доступна в [блоге](https://www.calabonga.net/blog/post/sozdaem-katalog-tovarov-na-asp-net-core).

## Дополнительные материалы по теме
* [Микросервисы 1](https://www.calabonga.net/blog/post/microservises-template)
* [Микросервисы 2](https://www.calabonga.net/blog/post/microservices-2-shablon-dlya-bystrogo-sozdaniya-mikroservisa-na-baze--net-core)
* [Канал youtube.com](https://www.youtube.com/sergeicalabonga)
