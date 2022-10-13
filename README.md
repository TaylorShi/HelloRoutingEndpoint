## 什么是路由

![](/Assets/2022-10-14-00-09-09.png)

**路由(Routing)负责匹配传入的HTTP请求，然后将这些请求发送到应用的可执行终结点(Endpoint)**。终结点是应用的可执行请求处理代码单元。终结点在应用中进行定义，并在应用启动时进行配置。终结点匹配过程可以从请求的URL中提取值，并为请求处理提供这些值。通过使用应用中的终结点信息，路由还能生成映射到终结点的URL。

> 应用可以使用以下内容配置路由：

- Controllers
- RazorPages
- SignalR
- gRPC服务
- 启用终结点的中间件，例如运行状况检查
- 通过路由注册的委托和Lambda

### 路由注册方式

> 路由系统的核心作用是，将访问URL和应用程序Controller形成一种映射关系，这种映射关系有两种作用：

- 把Url映射到对应的Controller的Action上
- 根据Controller和Action的名字来生成URL

> ASP.Net Core提供两种方式来注册路由

- 路由模板的方式，之前传统的方式，作为MVC页面的Web配置
- RouteAttribute的方式，更适合Web API的场景，更适合前后端分离的架构

## 相关文章

* [乘风破浪，遇见最佳跨平台跨终端框架.Net Core/.Net生态 - 浅析ASP.NET Core路由和终结点，规划Web API的最佳实践](https://www.cnblogs.com/taylorshi/p/16790159.html)