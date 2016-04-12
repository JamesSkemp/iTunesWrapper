using System;
using iTunesLib;
using System.Data;

namespace JamesRSkemp.iTunes.Wrapper {
	public class iTunesWrapper {
		internal iTunesApp iTunes = new iTunesAppClass();

		public class iTunesTrack {
			public string Album;
			public string Artist;
			public string Comment;
			/// <summary>
			/// Is this track part of a compilation album?
			/// </summary>
			public bool Compilation;
			public DateTime DateAdded;
			public int DiscCount;
			public int DiscNumber;
			public int Duration;
			public string Genre;
			public int Index;
			public string Kind;
			public string Name;
			public int PlayedCount;
			public DateTime PlayedDate;
			public int Rating;
			public string Time;
			public int TrackCount;
			public int TrackId;
			public int TrackNumber;
			public int Year;
		}

		/// <summary>
		/// Returns all sources currently loaded into iTunes.
		/// </summary>
		/// <returns>DataTable with source information.</returns>
		public DataTable GetSources() {
			DataTable dt = new DataTable();
			dt.Columns.Add("Index");
			dt.Columns.Add("Name");
			dt.Columns.Add("Kind");
			dt.Columns.Add("SourceId");
			//dt.Columns.Add("PlaylistId");
			//dt.Columns.Add("TrackDatabaseId");
			//dt.Columns.Add("TrackId");
			dt.Columns.Add("Capacity");
			dt.Columns.Add("FreeSpace");

			IITSourceCollection sources = iTunes.Sources;
			DataRow dr;

			foreach (IITSource source in sources) {
				dr = dt.NewRow();
				dr["Index"] = source.Index;
				dr["Name"] = source.Name;
				dr["Kind"] = source.Kind;
				dr["SourceId"] = source.sourceID;
				//dr["PlaylistId"] = source.playlistID;
				//dr["TrackDatabaseId"] = source.TrackDatabaseID;
				//dr["TrackId"] = source.trackID;
				dr["Capacity"] = source.Capacity;
				dr["FreeSpace"] = source.FreeSpace;
				dt.Rows.Add(dr);
				//source.Playlists;//IITPlaylistCollection
			}
			return dt;
		}

		/// <summary>
		/// Returns a DataTable with all tracks on a particular source.
		/// </summary>
		/// <param name="sourceId">Unique ID associated to sources by iTunes. Use GetSources method for listing.</param>
		/// <returns>DataTable with basic track information.</returns>
		public DataTable GetSourceTracks(int sourceId) {
			DataTable dt = new DataTable();
			dt.Columns.Add("Index");
			dt.Columns.Add("Name");
			dt.Columns.Add("Artist");
			dt.Columns.Add("Album");
			//dt.Columns.Add("");

			DataRow dr;

			IITSourceCollection sources = iTunes.Sources;
			foreach (IITSource source in sources) {
				if (source.sourceID == sourceId) {
					foreach (IITPlaylist playlist in source.Playlists) {
						if (playlist.Kind == ITPlaylistKind.ITPlaylistKindLibrary) {
							foreach (IITTrack track in playlist.Tracks) {
								dr = dt.NewRow();
								dr["Index"] = track.Index;
								dr["Name"] = track.Name;
								dr["Artist"] = track.Artist;
								dr["Album"] = track.Album;
								dt.Rows.Add(dr);
							}
							break;
						}
					}
					break;
				}
			}
			return dt;
		}

		/// <summary>
		/// Returns the version of iTunes used by the user.
		/// </summary>
		/// <returns>String with iTunes version.</returns>
		public string GetiTunesVersion() {
			return iTunes.Version;
		}

		/// <summary>
		/// Returns information about the currently playing track.
		/// </summary>
		/// <returns>iTunesTrack with current track's information.</returns>
		public iTunesTrack GetCurrentTrack() {
			IITTrack currentTrack = iTunes.CurrentTrack;
			iTunesTrack track = new iTunesTrack();
			track.Album = currentTrack.Album;
			track.Artist = currentTrack.Artist;
			track.Comment = currentTrack.Comment;
			track.Compilation = currentTrack.Compilation;
			track.DateAdded = currentTrack.DateAdded;
			track.DiscCount = currentTrack.DiscCount;
			track.DiscNumber = currentTrack.DiscNumber;
			track.Duration = currentTrack.Duration;
			track.Genre = currentTrack.Genre;
			track.Index = currentTrack.Index;
			track.Kind = currentTrack.KindAsString;
			track.Name = currentTrack.Name;
			track.PlayedCount = currentTrack.PlayedCount;
			track.PlayedDate = currentTrack.PlayedDate;
			track.Rating = currentTrack.Rating;
			track.Time = currentTrack.Time;
			track.TrackCount = currentTrack.TrackCount;
			track.TrackId = currentTrack.trackID;
			track.TrackNumber = currentTrack.TrackNumber;
			track.Year = currentTrack.Year;
			return track;
		}

		internal void Testing() {

		}

	}
}
