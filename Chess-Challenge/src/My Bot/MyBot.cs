using ChessChallenge.API;
using System;
using System.Collections.Generic;

public class MyBot : IChessBot
{
    int[] pieceValues = { 0, 100, 300, 300, 500, 900, 10000 };

    public Move Think(Board board, Timer timer)
    {
        Move[] legalMoves = board.GetLegalMoves();
        List<Move> possibleMoves = new List<Move>(legalMoves);

        foreach (Move move in legalMoves)
        {
            if (MoveIsCheckmate(board, move))
            {
                return move;
            }
            else if (MoveIsCheck(board, move))
            {
                if (!board.SquareIsAttackedByOpponent(move.TargetSquare))
                {
                    return move;
                }
            }
            else if (move.IsCapture)
            {
                if (!board.SquareIsAttackedByOpponent(move.TargetSquare))
                {
                    return move;
                }
                else if (pieceValues[(int)move.CapturePieceType] >= pieceValues[(int)move.MovePieceType])
                {
                    return move;
                }
            }
            else if (board.SquareIsAttackedByOpponent(move.TargetSquare))
            {
                possibleMoves.Remove(move);
            }
        }

        Random rng = new();

        if (possibleMoves.Count > 0)
        {
            return possibleMoves[rng.Next(possibleMoves.Count)];
        }
        else
        {
            return legalMoves[rng.Next(legalMoves.Length)];
        }
    }

    bool MoveIsCheckmate(Board board, Move move)
    {
        board.MakeMove(move);
        bool isCheckmate = board.IsInCheckmate();
        board.UndoMove(move);

        return isCheckmate;
    }

    bool MoveIsCheck(Board board, Move move)
    {
        board.MakeMove(move);
        bool isCheck = board.IsInCheck();
        board.UndoMove(move);

        return isCheck;
    }
}