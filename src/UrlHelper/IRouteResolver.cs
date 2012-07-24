namespace UrlHelper
{
	public interface IRouteResolver
	{
		void Initialize(IResolver resolver, string area);
	}
}