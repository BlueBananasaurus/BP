using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Monogame_GL
{
    public class RainDrop : IParticle
    {
        private Vector2 _velocity;
        private Vector2 _position;
        private Vector2 _oldPosition;
        private LineObject _track;
        private Vector2 _offset;

        public RectangleF Boundary
        {
            get
            {
                return _track.Line.LineBoundingBox();
            }

            set
            {
            }
        }

        public RainDrop(Vector2 velocity, Vector2 position)
        {
            _velocity = velocity;
            _position = position;
            _oldPosition = _position;
            _track = new LineObject(Game1.mapLive, new LineSegmentF(_oldPosition, _position));
        }

        public void Update()
        {
            _oldPosition = _position;
            _offset = Vector2.Normalize(_velocity) * 32;
            _position += _velocity * Game1.Delta;
            _track.Line.Start = _oldPosition;
            _track.Line.End = _position + _offset;

            List<Vector2Object> intersectionsPlayer = CompareF.LineIntersectionRectangle(Game1.PlayerInstance.Boundary, new LineObject(Game1.PlayerInstance, _track.Line));

            //if (intersectionsPlayer != null && intersectionsPlayer.Count > 0)
            //{
            //    Game1.mapLive.mapParticles.Remove(this);
            //}

            //foreach (Inpc npc in npcs)
            //{
            //    List<VectorObject> intersections = CompareF.LineIntersectionRectangle(npc, new LineObject(npc, _track.Line));

            //    if (intersections != null && intersections.Count > 0)
            //    {
            //        Game1.mapLive.mapParticles.Remove(this);
            //        break;
            //    }
            //    else if (CompareF.RectangleVsVector2(npc.Boundary, _track.Line.End) == true)
            //    {
            //        Game1.mapLive.mapParticles.Remove(this);
            //        break;
            //    }
            //}

            if (CompareF.LineVsMap(Game1.mapLive.MapTree, _track).Count > 0)
            {
                Game1.mapLive.mapParticles.Remove(this);
            }

            //foreach (IRectangleGet rec in elevators)
            //{
            //    List<VectorObject> intersections = CompareF.LineIntersectionRectangle(rec,new LineObject(rec, _track.Line));

            //    if (intersections !=null && intersections.Count>0)
            //    {
            //        Game1.mapLive.mapParticles.Remove(this);
            //    }
            //    else if(CompareF.RectangleVsVector2(rec, _track.Line.End) == true)
            //    {
            //        Game1.mapLive.mapParticles.Remove(this);
            //    }
            //}

            if (CompareF.LineIntersectionRectangle(Game1.mapLive.MapBoundary, _track).Count > 0)
            {
                Game1.mapLive.mapParticles.Remove(this);
            }

            if (CompareF.RectangleFVsRectangleF(Camera2DGame.Boundary, _track.Line.LineBoundingBox()) == false)
            {
                Game1.mapLive.mapParticles.Remove(this);
            }
        }

        public void Draw()
        {
            if (CompareF.RectangleFVsRectangleF(Camera2DGame.Boundary, _track.Line.LineBoundingBox()) == true)
                Game1.SpriteBatchGlobal.Draw(Game1.rainDrop, _position, null, null, new Vector2(0, 1), CompareF.VectorToAngle(_velocity), null, Color.White, SpriteEffects.None);
        }

        public void DrawLight()
        {
        }

        public void Push(Vector2 velocity)
        {
        }

        public void DrawNormal()
        {

        }
    }
}