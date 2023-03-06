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
1. `Name` должно быть не менее 5 и не более 50 символов.
2. `Description` должно быть не более 1024 символов, но может быть пустым.
3. `Category` можно создать без товаров.
4. `Category` можно выключить/выключить (скрыть/показать для всеобщего просмотра).
5. При выключении каталога все товары в каталоге тоже должны выключиться.
6. При включении каталога необходимо явно указать, включать или не включать товары.
7. Просмотр всех каталогов должно использоваться разбиение на страницы (paging)
8. При создании нового каталога, он должен быть невидимый по умолчанию.
9. API должна содержать методы CRUD для управления сущностью `Category`:
	* [ ] `GetPaged(int pageIndex, int pageSize)`
	* [x] `GetAll()`
	* [ ] `Create(CategoryViewModel model)`
	* [ ] `GetById(Guid id)`
	* [ ] `Update(CategoryUpdateViewModel)`
	* [ ] `Delete(Guid id)`
---
## Сущность "Product"
1. `Name` должно быть не менее 5 и не более 128 символов.
2. `Description` должно быть не более 2048 символов, но может быть пустым.
3. `Price` может быть не задана (null).
4. `CategoryId` обязательно при создании нового товара.
5. `Product` можно выключить/выключить (скрыть/показать для всеобщего просмотра).
6. Товар нельзя включить, если каталог товара выключен
7. Просмотр всех товаров должно использоваться разбиение на страницы (paging)
8. При создании товар он должен быть невидимый.
9. API должна содержать методы CRUD для управления сущностью `Product`:
	* [ ] `GetPaged(int pageIndex, int pageSize)`
	* [ ] `GetAll()`
	* [ ] `Create(ProductViewModel model)
	* [ ] `GetById(Guid id)`
	* [ ] `Update(ProductUpdateViewModel)`
	* [ ] `Delete(Guid id)`
	* [ ] `GetMostReviewed(int count)`
	* [ ] `GetMostRated(int count)`
---
## Сущность "Review"
1. `UserName` должно быть не менее 5 и не более 128 символов
2. `Content` должно быть не более 2048 символов, но может быть пустым
3. `Rating` должно быть от 1 до 5 единиц (очков, баллов, и т.д.)
4. `ProductId` обязательно при создании нового обзора (комментария)
5. Посмотреть все обзоры для товаров можно лишь только администратору.
6. Просмотр всех обзоров должны использоваться разбиение на страницы (paging)
7. `Review` можно тоже выключить и включить в соответствии со статусом товара, для которого это review написано.
8. API должна содержать методы CRUD для управления сущностью `Review`:
	* [ ] `Create(ReviewViewModel model)`
	* [ ] `GetById(Guid id)`
	* [ ] `Update(ReviewUpdateViewModel)`
	* [ ] `Delete(Guid id)`
	* [ ] `GetLastReview(int count)`
	* [ ] `GetPaged(int pageIndex, int pageSize)`
	* [ ] `GetAll(Guid productId)`
---
## Сущность "Tag" ("Метка")
1. Один продукт должен иметь одну и более меток (до 8 шт).
2. При создании и при редактировании товара к нему можно добавить несколько меток. Если метка не существует в системе, то она создается. Если метка уже существует, то к товару привязывается ссылка нее.
3. Если из описания товара удаляется метка, то надо проверить что эта метка не используется других товарах. Если метка больше нигде не используется, ее требуется удалить.
4. Данные о продукте должны включать в себя метки товара (`GetById` и `GetPaged`)
5. Просмотр всех меток, которые используются в каталоге можно осуществить на странице "Облако меток" (см. `GetCloud()`).
6. API должна содержать методы CRUD для управления сущностью `Tag`:
	* [ ] `GetCloud()`
	* [ ] `Update(TagUpdateViewModel)`
	* [ ] `Delete(Guid id)`
---

## Общие требования для сущности Review

1. Товар может иметь несколько отзывов или не иметь вообще.
2. Отзыв может оставить только зарегистрированный пользователь ролью `User`
3. Отзыв должен содержать следующие обязательные свойства: `Id`, `Content`, `Rating`, `UserName`
4. Список последних 10 отзывов может быть также запрошен на UI (см. `GetLastReview(int count)`).
5. При сокрытии товара от всеобщего просмотра, отзывы о товаре тоже не должны нигде отображаться.
---

## Требования для роли Администратор

1. Администратор должен иметь возможность удалить любой отзыв
2. Администратор должен иметь возможность удалять товары
3. Администратор должен иметь возможность удалять пользователей
4. Администратор должен иметь возможность удалять метки
---

## Требования для роли Пользователь

1. Пользователь может добавить сколько угодно отзывов на любой товар
2. Пользователь может удалить свой и только свой отзыв
3. Пользователь может изменить рейтинг своего отзыва и текст содержания

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
