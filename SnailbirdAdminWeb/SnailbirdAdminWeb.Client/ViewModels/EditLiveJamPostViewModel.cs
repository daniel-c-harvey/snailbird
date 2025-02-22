﻿using RazorCore.Table;
using SnailbirdData.Models.Post;

namespace SnailbirdAdminWeb.Client.ViewModels
{
    public class EditLiveJamPostViewModel : EditPostViewModelBase<LiveJamPost, EditLiveJamPostViewModel>
    {
        private List<Instrument> _instruments = new List<Instrument>();
        public IEnumerable<Instrument> Instruments => _instruments;

        public static IColumnMap<Instrument> InstrumentColumns = new ColumnMap<Instrument>()
            .AddColumn(
                ColumnKey.Init(typeof(Instrument).GetProperty(nameof(Instrument.Name))),
                new ModelColumn<Instrument, string>(
                    inst => inst.Name,
                    (inst, name) => inst.Name = name)
                .WithEditable())
            .AddColumn(
                ColumnKey.Init(typeof(Instrument).GetProperty(nameof(Instrument.Description))),
                new ModelColumn<Instrument, string>(
                    inst => inst.Description,
                    (inst, desc) => inst.Description = desc)
                .WithEditable());

        public EditLiveJamPostViewModel(Action<LiveJamPost> onCommitPost) 
        : base(onCommitPost) 
        { }

        public void AddNewInstrument(Instrument instrument)
        {
            _instruments.Add(instrument);
            InstrumentsModified();
        }

        private void InstrumentsModified()
        {
            if (Post != null)
            {
                Post.Instruments = _instruments;
            }
        }

        public void RemoveInstrument(Instrument instrument)
        {
            _instruments.Remove(instrument);
            InstrumentsModified();
        }

        public override EditLiveJamPostViewModel LoadPost(LiveJamPost post)
        {
            base.LoadPost(post);
            
            if (Post != null)
            {
                _instruments = Post.Instruments.ToList();
            }

            return this;
        }
    }
}

