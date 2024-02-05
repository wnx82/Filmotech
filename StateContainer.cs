using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Filmotech.Entities;


namespace Filmotech
{
    public class StateContainer
    {


        #region Films
        private List<Film> films = new();
        private Film? selectedFilm = null;

        public List<Film> Films
        {
            get => films;
            set
            {
                films = value;
                NotifyFilmsUpdated();
            }
        }

        public Film? SelectedFilm
        {
            get => selectedFilm;
            set
            {
                selectedFilm = value;
                OnFilmSelected?.Invoke();
            }
        }

        public event Action? OnFilmUpdated;
        public event Action? OnFilmSelected;

        public void NotifyFilmsUpdated() => OnFilmUpdated?.Invoke();
        #endregion
    }
}