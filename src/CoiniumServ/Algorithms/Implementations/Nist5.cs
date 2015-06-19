﻿#region License
// 
//     CoiniumServ - Crypto Currency Mining Pool Server Software
//     Copyright (C) 2013 - 2014, CoiniumServ Project - http://www.coinium.org
//     http://www.coiniumserv.com - https://github.com/CoiniumServ/CoiniumServ
// 
//     This software is dual-licensed: you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
// 
//     This program is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
//    
//     For the terms of this license, see licenses/gpl_v3.txt.
// 
//     Alternatively, you can license this software under a commercial
//     license or white-label it as set out in licenses/commercial.txt.
// 
#endregion

using System.Collections.Generic;
using HashLib;

namespace CoiniumServ.Algorithms.Implementations
{
    public sealed class Nist5 : IHashAlgorithm
    {
        public uint Multiplier { get; private set; }

        private readonly List<IHash> _hashers;

        public Nist5()
        {
            _hashers = new List<IHash>
            {
                HashFactory.Crypto.SHA3.CreateBlake512(),
                HashFactory.Crypto.SHA3.CreateGroestl512(),
                HashFactory.Crypto.SHA3.CreateSkein512(),
                HashFactory.Crypto.SHA3.CreateJH512(),
                HashFactory.Crypto.SHA3.CreateKeccak512()
            };

            Multiplier = 1;
        }

        public byte[] Hash(byte[] input)
        {
            var buffer = input;

            foreach (var hasher in _hashers)
            {
                buffer = hasher.ComputeBytes(buffer).GetBytes();
            }

            return buffer;
        }
    }
}
