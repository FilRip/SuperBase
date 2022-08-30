using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace TestCallBaseBaseMethod
{
    /// <summary>
    /// Méthodes d'extensions pour les méthodes d'un type
    /// </summary>
    public static class ExtensionsMethodes
    {
        /// <summary>
        /// Execute directement la méthode (sans paramètres) supérieur (d'une classe parente) en cours de l'objet, en passant outre le remplacement (new ou override)<br/>
        /// Retourne ce que la méthode retourne (si ce n'est pas une void)
        /// </summary>
        /// <typeparam name="T">Classe parente ou exécuter la méthode</typeparam>
        /// <typeparam name="TRetour">Type du retour de la méthode</typeparam>
        /// <param name="instance">Instance de l'objet en cours</param>
        public static TRetour SuperBase<T, TRetour>(this T instance) where T : class
        {
            return (TRetour)SuperBase(instance, new StackTrace(false).GetFrames()[1].GetMethod().Name, new object[] { });
        }

        /// <summary>
        /// Execute directement la méthode supérieur (d'une classe parente) en cours de l'objet, en passant outre le remplacement (new ou override)<br/>
        /// Retourne ce que la méthode retourne (si ce n'est pas une void)
        /// </summary>
        /// <typeparam name="T">Classe parente ou exécuter la méthode</typeparam>
        /// <typeparam name="TRetour">Type du retour de la méthode</typeparam>
        /// <param name="instance">Instance de l'objet en cours</param>
        /// <param name="parametres">Liste du/des paramètres à passer à la méthode</param>
        public static TRetour SuperBase<T, TRetour>(this T instance, params object[] parametres) where T : class
        {
            return (TRetour)SuperBase(instance, new StackTrace(false).GetFrames()[1].GetMethod().Name, parametres);
        }

        /// <summary>
        /// Execute directement la méthode supérieur (d'une classe parente) spécifiée de l'objet, en passant outre le remplacement (new ou override)<br/>
        /// Retourne ce que la méthode retourne (si ce n'est pas une void)
        /// </summary>
        /// <typeparam name="T">Classe parente ou exécuter la méthode</typeparam>
        /// <typeparam name="TRetour">Type du retour de la méthode</typeparam>
        /// <param name="instance">Instance de l'objet en cours</param>
        /// <param name="parametres">Liste du/des paramètres à passer à la méthode</param>
        /// <param name="nomMethode">Nom de la méthode à exécuter</param>
        public static TRetour SuperBase<T, TRetour>(this T instance, string nomMethode = "", params object[] parametres) where T : class
        {
            return (TRetour)SuperBase(instance, nomMethode, parametres);
        }

        /// <summary>
        /// Execute directement la méthode (sans paramètres) supérieur (d'une classe parente) spécifiée de l'objet, en passant outre le remplacement (new ou override)<br/>
        /// Retourne ce que la méthode retourne (si ce n'est pas une void)
        /// </summary>
        /// <typeparam name="T">Classe parente ou exécuter la méthode</typeparam>
        /// <typeparam name="TRetour">Type du retour de la méthode</typeparam>
        /// <param name="instance">Instance de l'objet en cours</param>
        /// <param name="nomMethode">Nom de la méthode à exécuter</param>
        public static TRetour SuperBase<T, TRetour>(this T instance, string nomMethode = "") where T : class
        {
            return (TRetour)SuperBase(instance, nomMethode, new object[] { });
        }

        /// <summary>
        /// Execute directement la méthode (sans paramètres) supérieur (d'une classe parente) spécifiée de l'objet, en passant outre le remplacement (new ou override)
        /// </summary>
        /// <typeparam name="T">Classe parente ou exécuter la méthode</typeparam>
        /// <param name="instance">Instance de l'objet en cours</param>
        /// <param name="nomMethode">Nom de la méthode à exécuter</param>
        public static object SuperBase<T>(this T instance, string nomMethode) where T : class
        {
            return SuperBase(instance, nomMethode, new object[] { });
        }

        /// <summary>
        /// Execute directement la méthode (sans paramètres) supérieur (d'une classe parente) en cours de l'objet, en passant outre le remplacement (new ou override)
        /// </summary>
        /// <typeparam name="T">Classe parente ou exécuter la méthode</typeparam>
        /// <param name="instance">Instance de l'objet en cours</param>
        public static object SuperBase<T>(this T instance) where T : class
        {
            return SuperBase(instance, new StackTrace(false).GetFrames()[1].GetMethod().Name, new object[] { });
        }

        /// <summary>
        /// Execute directement la méthode supérieur (d'une classe parente) en cours de l'objet, en passant outre le remplacement (new ou override)
        /// </summary>
        /// <typeparam name="T">Classe parente ou exécuter la méthode</typeparam>
        /// <param name="instance">Instance de l'objet en cours</param>
        /// <param name="parametres">Liste du/des paramètres à passer à la méthode</param>
        public static object SuperBase<T>(this T instance, params object[] parametres) where T : class
        {
            return SuperBase(instance, new StackTrace(false).GetFrames()[1].GetMethod().Name, parametres);
        }

        /// <summary>
        /// Execute directement la méthode supérieur (d'une classe parente) spécifiée de l'objet, en passant outre le remplacement (new ou override)
        /// </summary>
        /// <typeparam name="T">Classe parente ou exécuter la méthode</typeparam>
        /// <param name="instance">Instance de l'objet en cours</param>
        /// <param name="parametres">Liste du/des paramètres à passer à la méthode</param>
        /// <param name="nomMethode">Nom de la méthode à exécuter</param>
        public static object SuperBase<T>(this T instance, string nomMethode = "", params object[] parametres) where T : class
        {
            Type classeParente = typeof(T);
            if (string.IsNullOrWhiteSpace(nomMethode))
                nomMethode = new StackTrace(false).GetFrames().First(item => item.GetMethod().Name != nameof(SuperBase)).GetMethod().Name;
            MethodBase mb = classeParente.GetMethod(nomMethode, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            if (mb == null)
            {
                object param = nomMethode;
                nomMethode = new StackTrace(false).GetFrames().First(item => item.GetMethod().Name != nameof(SuperBase)).GetMethod().Name;
                mb = classeParente.GetMethod(nomMethode, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                if (mb != null)
                {
                    Array.Resize(ref parametres, parametres.Length + 1);
                    parametres[parametres.Length - 1] = param;
                }
            }
            if (mb == null)
                throw new Exception("Méthode " + nomMethode + " non trouvée");
            if (!mb.IsStatic && instance == null)
                throw new Exception("Méthode non static, vous devez spécifier l'instance de l'objet");
            if (mb.IsPrivate && !mb.IsFamilyOrAssembly)
                throw new Exception("La méthode " + nomMethode + " est privée");
            if (!mb.IsStatic)
            {
                if (!instance.GetType().IsSubclassOf(classeParente))
                    throw new Exception("L'objet " + instance.GetType() + " ne dérive pas de la classe " + classeParente.ToString());
            }
            IntPtr pointeurMethode = mb.MethodHandle.GetFunctionPointer();
            Type typeDeRetour = typeof(void);
            if (mb is MethodInfo mi)
            {
                if (mi.ReturnType != typeof(void))
                    typeDeRetour = mi.ReturnType;
            }
            else if (mb is ConstructorInfo ci)
                typeDeRetour = ci.DeclaringType;

            Type[] listeParametres = Type.EmptyTypes;

            if (parametres != null && parametres.Length > 0)
                listeParametres = parametres.Select(param => param.GetType()).ToArray();

            if (listeParametres.Length > mb.GetParameters().Length)
                throw new Exception("Trop d'arguments (" + listeParametres.Length.ToString() + ") pour la méthode '" + nomMethode + "'(" + mb.GetParameters().Length + ")");

            if (typeDeRetour == typeof(void))
            {
                Type monAction = Expression.GetActionType(listeParametres);
                var exec = Activator.CreateInstance(monAction, instance, pointeurMethode);
                ((Delegate)exec).DynamicInvoke(parametres);
                return null;
            }
            else
            {
                Array.Resize(ref listeParametres, listeParametres.Length + 1);
                listeParametres[listeParametres.Length - 1] = typeDeRetour;
                Type maFunc = Expression.GetFuncType(listeParametres);
                var exec = Activator.CreateInstance(maFunc, instance, pointeurMethode);
                return Convert.ChangeType(((Delegate)exec).DynamicInvoke(parametres), typeDeRetour);
            }
        }
    }
}
