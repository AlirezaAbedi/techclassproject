using System.Text.Json.Nodes;

namespace WebApi.Mock
{
    public static class RouteMiddlewareExtensions
    {
        public static WebApplication UseExtraRoutes(this WebApplication app)
        {
            var writableDoc = JsonNode.Parse(File.ReadAllText("Mock/Mock.json"));
            foreach (var elem in writableDoc?.Root.AsObject().AsEnumerable())
            {
                var arr = elem.Value.AsArray();
                app.MapGet(string.Format("/api/v1/{0}", elem.Key), () => elem.Value.ToString());
                app.MapGet(string.Format("/api/v1/{0}", elem.Key) + "/{id}", (int id) =>
                {
                    var matchedItem = arr.SingleOrDefault(row => row
                      .AsObject()
                      .Any(o => o.Key.ToLower() == "id" && int.Parse(o.Value.ToString()) == id)
                    );
                    return matchedItem;
                });
                app.MapPost(string.Format("/api/v1/{0}", elem.Key), async (HttpRequest request) => {
                    string content = string.Empty;
                    using (StreamReader reader = new StreamReader(request.Body))
                    {
                        content = await reader.ReadToEndAsync();
                    }
                    var newNode = JsonNode.Parse(content);
                    var array = elem.Value.AsArray();
                    newNode.AsObject().Add("Id", array.Count() + 1);
                    array.Add(newNode);

                    File.WriteAllText("Mock/mock.json", writableDoc.ToString());
                    return content;
                });
                app.MapPut(string.Format("/api/v1/{0}", elem.Key), () => {
                    return "TODO";
                });
                app.MapDelete(string.Format("/api/v1/{0}", elem.Key) + "/{id}", (int id) => {

                    var matchedItem = arr
                     .Select((value, index) => new { value, index })
                     .SingleOrDefault(row => row.value
                      .AsObject()
                      .Any(o => o.Key.ToLower() == "id" && int.Parse(o.Value.ToString()) == id)
                    );
                    if (matchedItem != null)
                    {
                        arr.RemoveAt(matchedItem.index);
                        File.WriteAllText("mock.json", writableDoc.ToString());
                    }

                    return "OK";
                });
            }
            return app;
        }
    }
}
